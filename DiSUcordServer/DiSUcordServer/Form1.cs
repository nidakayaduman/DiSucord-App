using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace DiSUcordServer
{
    public partial class Form1 : Form
    {
        private HashSet<string> connectedUsernames = new HashSet<string>();
        private Dictionary<TcpClient, string> clientUsernames = new Dictionary<TcpClient, string>();
        private HashSet<TcpClient> subscribersIF100 = new HashSet<TcpClient>();
        private HashSet<TcpClient> subscribersSPS101 = new HashSet<TcpClient>();
        private Dictionary<TcpClient, StreamWriter> clientWriters = new Dictionary<TcpClient, StreamWriter>();
        private TcpListener listener;

        //initialized the component and disconnect buttons started as disabled.
        public Form1()
        {
            InitializeComponent();
            btndisconnect.Enabled = false;
            rtbserveractivity.ReadOnly = true;
            rtbunsuccesfulattempts.ReadOnly = true;
            rtbsendingmessages.ReadOnly = true;
            rtbif100subscribers.ReadOnly = true;
            rtbcurrentlyconnectedclients.ReadOnly = true;
            rtbclientsunsubscribingchannels.ReadOnly = true;
            rtbclientssubscribingchannels.ReadOnly = true;
            rtbsps101subscribers.ReadOnly = true;
            rtbdisconnections.ReadOnly = true;


        }

        // this function is basically responsible for connect button of the server side.
        // creates a new thread and starts it at the written port. 
        // if the user enters an invalid port, a message box appears and says please enter a valid port number.

        private void btnconnect_Click(object sender, EventArgs e)
        {
            int port;
            if (int.TryParse(tbportnumber.Text, out port))
            {
               
                Thread serverThread = new Thread(() => StartServer(port));
                serverThread.IsBackground = true;
                serverThread.Start();
                this.Invoke((MethodInvoker)delegate
                {
                    rtbserveractivity.Clear();
                    rtbunsuccesfulattempts.Clear();
                    rtbif100subscribers.Clear();
                    rtbsps101subscribers.Clear();
                    rtbsendingmessages.Clear();
                    rtbdisconnections.Clear();
                    rtbclientssubscribingchannels.Clear();
                    rtbclientsunsubscribingchannels.Clear();
                    rtbcurrentlyconnectedclients.Clear();
                   
                });
            }

            else
            {
                MessageBox.Show("Please enter valid port number.");
            }


        }

        // Starts a TCP server on the specified port, listens for incoming client connections,
        /// and launches a new thread to handle each connected client.
        
        private void StartServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);

            try
            {
                listener.Start();
                this.Invoke((MethodInvoker)delegate
                {
                    rtbserveractivity.AppendText($"Server started on port {port}.\n");
                    btnconnect.Enabled = false;
                    btndisconnect.Enabled = true;
                });
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();

                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                    clientThread.IsBackground = true;
                    clientThread.Start(client);
                }
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    rtbserveractivity.AppendText($"Server could not start on port: {ex.Message}\n");
                    rtbunsuccesfulattempts.AppendText($"Server could not start on port: {ex.Message}\n");
                    btnconnect.Enabled = true;
                    btndisconnect.Enabled = false;
                });
            }

        }

        // Handles communication with a connected client. Reads the client's username,
        // processes login attempts, manages user subscriptions to channels, and handles
        // incoming messages. Also monitors client disconnections and performs cleanup.

        private void HandleClient(object clientObj)
        {
            TcpClient client = (TcpClient)clientObj;
            string username = null;

            try
            {
                // Set up stream readers and writers for communication with the client.
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream) { AutoFlush = true })
                {
                    username = reader.ReadLine();

                    if (!string.IsNullOrEmpty(username))
                    {
                        lock (connectedUsernames)
                        {
                            // Update server state with the new connection.
                            if (!connectedUsernames.Contains(username))
                            {
                                connectedUsernames.Add(username);
                                clientUsernames[client] = username;
                                writer.WriteLine("OK");
                                writer.Flush();
                                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                string loginMessage = $"{timestamp} {username} logged in.";
                                Invoke(new Action(() =>
                                {
                                    rtbcurrentlyconnectedclients.AppendText(username + Environment.NewLine);
                                    rtbserveractivity.AppendText(loginMessage + Environment.NewLine);

                                }));
                            }
                            else

                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    // Notify the client that the username is already taken.
                                    rtbunsuccesfulattempts.AppendText($"Username {username} is already taken." + Environment.NewLine);
                                });
                                writer.WriteLine("Username is already taken.");
                                return;
                            }
                        }

                        // Process client commands and messages.
                        string command;
                        try
                        {
                            while ((command = reader.ReadLine()) != null)
                            {

                                if (command == "LOGOUT")
                                {
                                    HandleLogout(client, clientUsernames[client]);
                                    writer.WriteLine("LOGOUT_SUCCESS");
                                    writer.Flush();
                                    break;
                                }
                                else
                                {
                                    // Parse and handle messages or channel subscriptions.

                                    string[] parts = command.Split(':');
                                    if (parts.Length == 2 && (parts[0] == "SUBSCRIBE" || parts[0] == "UNSUBSCRIBE"))
                                    {
                                        string action = parts[0];
                                        string channelName = parts[1];

                                        if (action == "SUBSCRIBE")
                                        {
                                            SubscribeToChannel(client, channelName, writer);
                                        }
                                        else if (action == "UNSUBSCRIBE")
                                        {
                                            UnsubscribeFromChannel(client, channelName, writer);
                                        }
                                    }
                                    else 
                                    {
                                        string channel = parts[0];
                                        string message = parts[1];
                                        DistributeMessage(client, channel, message);
                                    }
                                }
                            }
                        }
                        catch (IOException ex)
                        {
                            string clientUsername = clientUsernames.TryGetValue(client, out var tempUsername) ? tempUsername : "Unknown";
                            string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            string errorMessage = $"[{currentTime}] An unexpected network error occurred.";

                            if (ex.Message.Contains("WSACancelBlockingCall"))
                            {
                                errorMessage = $"[{currentTime}] Server suddenly disconnected.";
                            }
                            else if (ex.Message.Contains("forcibly") || ex.Message.Contains("bilgisayar tarafından zorla"))
                            {
                                errorMessage = $"[{currentTime}] Client {clientUsername} suddenly exited.";
                            }

                            Invoke(new Action(() =>
                            {
                                rtbserveractivity.AppendText(errorMessage + Environment.NewLine);
                            }));

                            if (clientUsernames.ContainsKey(client))
                            {
                                HandleLogout(client, clientUsername); 
                            }
                        }
                        catch (SocketException ex)
                        {
                            HandleLogout(client, clientUsernames[client]);
                            string errorMessage = $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] ";
                            errorMessage += ex.Message;
                            Console.WriteLine(errorMessage);
                            rtbserveractivity.AppendText(errorMessage);


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string errorMessage = $"Error occurred at {currentTime} with client {username}: {ex}\n"; // ex.Message yerine ex kullanıldı

                Invoke(new Action(() =>
                {
                    rtbserveractivity.AppendText(errorMessage);
                    rtbunsuccesfulattempts.AppendText(errorMessage);
                }));
            }

        }

        // Handles the logout of a connected client. Removes the client from the server's
        // tracking structures, such as connected usernames and channel subscribers. Updates
        // UI elements to reflect the client's logout and unsubscribed channels.

        private void HandleLogout(TcpClient client, string username)
        {
            bool removedFromIF100, removedFromSPS101;
            // Get the current timestamp.
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            // Remove the client from the server's tracking structures.
            lock (connectedUsernames)
            {
                connectedUsernames.Remove(username);
                clientUsernames.Remove(client);
            }

            lock (subscribersIF100)
            {
                removedFromIF100 = subscribersIF100.Remove(client);
            }
            lock (subscribersSPS101)
            {
                removedFromSPS101 = subscribersSPS101.Remove(client);
            }

            Invoke(new Action(() =>
            {
                if (removedFromIF100)
                {
                    // Update the IF100 subscribers list and display channel-specific activity.
                    var if100subscribersList = subscribersIF100.Select(c => clientUsernames[c]).ToList();
                    if100subscribersList.Remove(username);
                    rtbif100subscribers.Text = String.Join("\n", if100subscribersList);
                    rtbclientsunsubscribingchannels.AppendText($"{timestamp} {username} unsubscribed from IF100.\n");
                }
                if (removedFromSPS101)
                {
                    // Update the SPS101 subscribers list and display channel-specific activity.
                    var sps101subscribersList = subscribersSPS101.Select(c => clientUsernames[c]).ToList();
                    sps101subscribersList.Remove(username);
                    rtbsps101subscribers.Text = String.Join("\n", sps101subscribersList);
                    rtbclientsunsubscribingchannels.AppendText($"{timestamp} {username} unsubscribed from SPS101.\n");
                }

                // Update the currently connected clients list.
                var currentlyConnectedList = rtbcurrentlyconnectedclients.Lines.ToList();
                currentlyConnectedList.Remove(username);
                rtbcurrentlyconnectedclients.Lines = currentlyConnectedList.ToArray();

                // Log the logout and disconnection activities.
                rtbserveractivity.AppendText($"{timestamp} {username} logged out.\n");
                rtbdisconnections.AppendText($"{timestamp} {username} disconnected.\n");
            }));
        }

        // Subscribes a connected client to a specific channel, updates server state, and notifies
        // the client of successful subscription. Also updates UI elements to reflect the subscription.
        private void SubscribeToChannel(TcpClient client, string channelName, StreamWriter writer)
        {
            // Get the username of the subscribing client and the current timestamp.
            string username = clientUsernames[client];
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Action updateUI = () =>
            {
                if (channelName == "IF100" && subscribersIF100.Add(client))
                {
                    // Update the IF100 subscribers list and display subscription activity.
                    rtbif100subscribers.AppendText(username + Environment.NewLine);
                    rtbclientssubscribingchannels.AppendText($"{timestamp} {username} subscribed to IF100.\n");
                }
                else if (channelName == "SPS101" && subscribersSPS101.Add(client))
                {
                    // Update the SPS101 subscribers list and display subscription activity.
                    rtbsps101subscribers.AppendText(username + Environment.NewLine);
                    rtbclientssubscribingchannels.AppendText($"{timestamp} {username} subscribed to SPS101.\n");
                }
            };
            // Notify the client of successful subscription.
            Invoke(updateUI);
            writer.WriteLine("SUBSCRIBED:" + channelName);
            // Notify other channel subscribers of the subscribing user's activity.
            NotifyChannelSubscribersOfUserActivity(clientUsernames[client], channelName, true);

        }
        // Unsubscribes a connected client from a specific channel, updates server state, and notifies
        // the client of successful unsubscription. Also updates UI elements to reflect the unsubscription.
        private void UnsubscribeFromChannel(TcpClient client, string channelName, StreamWriter writer)
        {
            string username = clientUsernames[client];
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Action updateUI = () =>
            {
                if (channelName == "IF100" && subscribersIF100.Remove(client))
                {
                    rtbif100subscribers.Text = String.Join(Environment.NewLine, subscribersIF100.Select(c => clientUsernames[c]));
                    rtbclientsunsubscribingchannels.AppendText($"{timestamp} {username} unsubscribed from IF100.\n");
                }
                else if (channelName == "SPS101" && subscribersSPS101.Remove(client))
                {
                    rtbsps101subscribers.Text = String.Join(Environment.NewLine, subscribersSPS101.Select(c => clientUsernames[c]));
                    rtbclientsunsubscribingchannels.AppendText($"{timestamp} {username} unsubscribed from SPS101.\n");
                }
            };

            Invoke(updateUI);
            writer.WriteLine("UNSUBSCRIBED:" + channelName);
            NotifyChannelSubscribersOfUserActivity(clientUsernames[client], channelName, false);

        }

        // Notifies subscribers of a specific channel about a user's activity (connection or disconnection).
        // Generates a system message and distributes it to all subscribers of the specified channel.
        private void NotifyChannelSubscribersOfUserActivity(string username, string channelName, bool isSubscribe)
        {
            string activityType = isSubscribe ? "has connected to" : "has disconnected from";
            string systemMessage = $"{username} {activityType} the channel.";
            // Distribute the system message to all subscribers of the specified channel.
            DistributeMessage(null, channelName, systemMessage, true);
        }


        // Distributes a message to all subscribers of a specified channel, including the sender.
        // Handles both regular messages and system messages, logs messages, and manages StreamWriter instances.
        private void DistributeMessage(TcpClient senderClient, string channel, string message, bool isSystemMessage = false)
        {
            // Get the list of subscribers for the specified channel.
            List<TcpClient> subscribers = GetSubscribersOfChannel(channel);
            // Determine the sender's username, considering system messages and null sender clients.
            string senderUsername = (isSystemMessage || senderClient == null) ? "CONNECTION" : clientUsernames[senderClient];

            // Log the message if it is not a system message.
            if (!isSystemMessage)
            {
                LogMessage(channel, senderUsername, message);
            }

            foreach (TcpClient subscriber in subscribers)
            {
                if (subscriber != null && subscriber.Connected)
                {
                    StreamWriter subscriberWriter;
                    // Get or create a StreamWriter for the subscriber.
                    if (!clientWriters.TryGetValue(subscriber, out subscriberWriter))
                    {
                        subscriberWriter = new StreamWriter(subscriber.GetStream()) { AutoFlush = true };
                        clientWriters[subscriber] = subscriberWriter;
                    }

                    try
                    {
                        // Determine the type of message (system or regular) and send it to the subscriber.
                        string messageType = isSystemMessage ? "CONNECTION" : "MESSAGE";
                        subscriberWriter.WriteLine($"{messageType}:{channel}:{senderUsername}: {message}");

                        // Notify the sender that their message was sent successfully.
                        if (subscriber == senderClient)
                        {
                            subscriberWriter.WriteLine("YOUR_MESSAGE_SENT_SUCCESSFULLY");
                        }
                    }
                    catch (Exception ex)
                    {
                        string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        string errorMessage = $"[{currentTime}] Exception occurred: {ex.Message}\n";

                      
                        rtbserveractivity.AppendText(errorMessage);

                        rtbunsuccesfulattempts.AppendText(errorMessage);

                        subscriberWriter.Close();
                        clientWriters.Remove(subscriber);
                        Console.WriteLine(ex.Message);
                        subscriber.Close();
                    }
                }
                else
                {
                    // Close StreamWriter and remove subscriber if it is not connected.
                    StreamWriter writerToRemove;
                    if (clientWriters.TryGetValue(subscriber, out writerToRemove))
                    {
                        writerToRemove.Close();
                    }
                    clientWriters.Remove(subscriber);
                    subscriber.Close();
                }
            }
        }

        /// Retrieves the list of TcpClients representing subscribers of a specific channel.
        private List<TcpClient> GetSubscribersOfChannel(string channel)
        {
            return channel == "IF100" ? subscribersIF100.ToList() : subscribersSPS101.ToList();
        }

        /// Logs a message to the server's message log, including the channel, sender's username, and message content.
        private void LogMessage(string channel, string username, string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.Invoke((MethodInvoker)delegate
            {
                rtbsendingmessages.AppendText($"{timestamp} {channel} {username}: {message}\n");
            });
        }

        // Event handler for the "Disconnect" button click. Disconnects all connected clients,
        // enabling the "Connect" button and disabling the "Disconnect" button afterward.
        private void btndisconnect_Click(object sender, EventArgs e)
        {
            foreach (var client in clientUsernames.Keys.ToList())
            {
                DisconnectClient(client);
            }
            btnconnect.Enabled = true;
            btndisconnect.Enabled = false;
        }

        // Event handler for the form closing event. Disconnects all connected clients
        // and stops the server when the application is closed.
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Tüm client'ları döngüde gez ve disconnect et
            foreach (var client in clientUsernames.Keys.ToList())
            {
                DisconnectClient(client);
            }

            // Sunucuyu kapat
            if (listener != null)
            {
                listener.Stop();
            }
        }

        // Disconnects a specific client, removes it from tracking structures, sends a disconnect message,
        // and logs the disconnection in the server activity log. Also cleans up other subscriptions and handles logout.
        private void DisconnectClient(TcpClient client)
        {
            string username;
            lock (clientUsernames)
            {
                // Try to get the username associated with the client.
                if (clientUsernames.TryGetValue(client, out username))
                {
                    clientUsernames.Remove(client);
                    connectedUsernames.Remove(username);
                    // Check if the client is still connected before attempting to send a disconnect message.
                    if (client != null && client.Connected)
                    {
                        try
                        {
                            // Use a StreamWriter to send a disconnect message directly to the client.
                            using (StreamWriter writer = new StreamWriter(client.GetStream()))
                            {
                                writer.WriteLine("SERVER:DISCONNECT");
                                writer.Flush();
                            }
                        }
                        catch (Exception ex)
                        {
                            string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            string errorMessage = $"[{currentTime}] Error sending disconnect message to client {username}: {ex.Message}\n";

                            this.Invoke((MethodInvoker)delegate
                            {
                                rtbserveractivity.AppendText(errorMessage);

                                rtbunsuccesfulattempts.AppendText(errorMessage);
                            });
                        }


                    }

                    client.Close();

                
                    this.Invoke((MethodInvoker)delegate
                    {
                        string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        rtbserveractivity.AppendText($"{currentTime} - {username} disconnected.\n");
                     
                    });

                    // Clean up other subscriptions and handle logout for the disconnected client.

                    HandleLogout(client, username); 
                }
                else
                {
                    // Log an error if attempting to disconnect a client that is not in the dictionary.
                    string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string errorMessage = $"[{currentTime}] Tried to disconnect a client that is not in the dictionary.\n";

                    this.Invoke((MethodInvoker)delegate
                    {
                       
                        rtbunsuccesfulattempts.AppendText(errorMessage);
                    });
                }


            }
        }

    }

}
