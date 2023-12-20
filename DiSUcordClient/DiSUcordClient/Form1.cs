using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net;

namespace DiSUcordClient
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;
        private bool isSubscribedToIF100 = false;
        private bool isSubscribedToSPS101 = false;
     

        public Form1()
        {
            InitializeComponent();
            btnLogout.Enabled = false;
            btnsubscribeif100.Enabled = false;
            btnunsubscribeif100.Enabled = false;
            btnsubscribesps101.Enabled = false;
            btnunsubscribesps101.Enabled = false;
            btnsendmessageif100.Enabled = false;
            btnsendmessagesps101.Enabled = false;
            btnsendmessageif100.Enabled = false;
            btnsendmessagesps101.Enabled = false;
            tbMessageforif100.Enabled = false;
            tbMessageforsps101.Enabled = false;
            rtbif100channel.ReadOnly = true;
            rtbsps101channel.ReadOnly = true;
            rtbstatus.ReadOnly = true;



        }




        // Event handler for the "Login" button click. Attempts to connect to the server with the provided
        // IP address, port number, and username. Sends the username to the server and processes the response.
        private void btnlogin_Click(object sender, EventArgs e)
        {

            // Get the server IP address, port number, and username from the user inputs.
            string serverIp = tbIPaddress.Text;
            // int port = Convert.ToInt32(tbPortNumber.Text);

            // Validate the username.
            string username = tbUsername.Text.Trim();
          
            if (string.IsNullOrEmpty(username))
            {
                rtbstatus.AppendText("Username cannot be empty.\n");
                return;
            }

            // Validate and parse the port number.
            if (!int.TryParse(tbPortNumber.Text, out int port))
            {
                rtbstatus.AppendText("Please enter a valid port number.\n");
                return;
            }

            // Validate and parse the IP address.
            if (!IPAddress.TryParse(serverIp, out IPAddress ipAddress))
            {
                rtbstatus.AppendText("Please enter a valid IP address.\n");
                return;
            }

         
            Thread connectThread = new Thread(() =>
            {
                try
            {

                // Attempt to connect to the server.
                client = new TcpClient();
                client.Connect(serverIp, port);

                this.Invoke((MethodInvoker)delegate
                {

                // Send the username to the server.
                stream = client.GetStream();
                writer = new StreamWriter(stream) { AutoFlush = true };
                reader = new StreamReader(stream);

                writer.WriteLine(username);
                var response = reader.ReadLine();

                if (response == "Username is already taken.")
                {
                    rtbstatus.AppendText("Username is already taken, please try a different one.\n");
                    client.Close();
                }

                else if (response == "OK")
                {
                   // Successful login.
                    rtbstatus.Clear();
                    rtbstatus.AppendText("Successfully logged in.\n");
                    btnlogin.Enabled = false;
                    btnLogout.Enabled = true;
                    btnsubscribeif100.Enabled = true;
                    btnunsubscribeif100.Enabled = false;
                    btnsubscribesps101.Enabled = true;
                    btnunsubscribesps101.Enabled = false;
                    btnsendmessageif100.Enabled = true;
                    btnsendmessagesps101.Enabled = true;
                    btnsendmessageif100.Enabled = false;
                    btnsendmessagesps101.Enabled = false;
                   // Start listening for messages from the server.
                    StartListeningForMessages();
                }
                else
                {
                    rtbstatus.AppendText("Unexpected response from server. Please try again.\n");
                    client.Close();
                }
            });

            }
                catch (SocketException ex)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        rtbstatus.AppendText($"Failed to connect to the server, please try again: {ex.Message}\n");
                        client?.Close();
                        client = null;
                    });
                }
                catch (IOException ex)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        rtbstatus.AppendText($"An error occurred while communicating with the server: {ex.Message}\n");
                        client?.Close();
                        client = null;
                    });
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        rtbstatus.AppendText($"An unexpected error occurred: {ex.Message}\n");
                        client?.Close();
                        client = null;
                    });
                }
            });
            connectThread.IsBackground = true;
            connectThread.Start();

        }

        // Event handler for the "Subscribe to IF100" button click.
        // Sends a channel subscription request to the server for the "IF100" channel.
        private void btnsubscribeif100_Click(object sender, EventArgs e)
        {
            SendChannelSubscriptionRequest("IF100", true);
           
        }

        // Event handler for the "Unsubscribe to IF100" button click.
        // Sends a channel unsubscription request to the server for the "IF100" channel.
        private void btnunsubscribeif100_Click(object sender, EventArgs e)
        {
            SendChannelSubscriptionRequest("IF100", false);
            
        }

        // Event handler for the "Subscribe to SPS101" button click.
        // Sends a channel subscription request to the server for the "SPS101" channel.
        private void btnsubscribesps101_Click(object sender, EventArgs e)
        {
            SendChannelSubscriptionRequest("SPS101", true);
           
        }

        // Event handler for the "Unsubscribe to SPS101" button click.
        // Sends a channel unsubscription request to the server for the "SPS101" channel.
        private void btnunsubscribesps101_Click(object sender, EventArgs e)
        {
            SendChannelSubscriptionRequest("SPS101", false);
      
        }

        // Updates the status of the send message buttons based on the current channel subscriptions.
        // Enables or disables the "Send Message" buttons for IF100 and SPS101 channels accordingly.
        private void UpdateSendButtonStatus()
        {
            // IF100 kanalına abonelik durumuna göre IF100 için "Send" butonunu güncelle
            btnsendmessageif100.Enabled = isSubscribedToIF100;

            // SPS101 kanalına abonelik durumuna göre SPS101 için "Send" butonunu güncelle
            btnsendmessagesps101.Enabled = isSubscribedToSPS101;
        }

        // Sends a channel subscription or unsubscription request to the server.
        private void SendChannelSubscriptionRequest(string channelName, bool subscribe)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Not connected to server.");
                return;
            }

            try
            {
                // Construct the subscription or unsubscription command based on the request.
                string command = (subscribe ? "SUBSCRIBE:" : "UNSUBSCRIBE:") + channelName;
                writer.WriteLine(command);
                writer.Flush();
            }
            catch (Exception ex)
            {
                rtbstatus.AppendText("Failed to send request: " + ex.Message);
            }
        }

        // Updates the states of channel subscription buttons and "Send Message" buttons based on the channel name and subscription status.
        private void UpdateButtonStates(string channelName, bool subscribed)
        {
            if (channelName == "IF100")
            {
                btnsubscribeif100.Enabled = !subscribed;
                btnunsubscribeif100.Enabled = subscribed;
            }
            else if (channelName == "SPS101")
            {
                btnsubscribesps101.Enabled = !subscribed;
                btnunsubscribesps101.Enabled = subscribed;
            }
            UpdateSendButtonStatus(); // "Send" butonlarının durumunu da güncelle
        }

        // Event handler for the "Logout" button click. Sends a logout command to the server.
        private void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the client is connected to the server.
                if (client != null && client.Connected)
                {
                    // Send a logout command to the server.
                    writer.WriteLine("LOGOUT");
                    writer.Flush();

                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                rtbstatus.AppendText($"Failed to log out: {ex.Message}\n");

            }
        }

        // Resets the user interface to its initial state after logging out.
        private void ResetUI()
        {
            // Disable channel subscription and "Send Message" buttons.
            btnsubscribeif100.Enabled = false;
            btnunsubscribeif100.Enabled = false;
            btnsubscribesps101.Enabled = false;
            btnunsubscribesps101.Enabled = false;
            btnsendmessageif100.Enabled = false;
            btnsendmessagesps101.Enabled = false;
            btnLogout.Enabled = false;
            btnlogin.Enabled = true;
            // Disable the "Logout" button and enable the "Login" button.
            rtbstatus.Clear();
            tbMessageforif100.Clear();
            tbMessageforsps101.Clear();
            // Clear various UI elements.
            rtbstatus.AppendText("Disconnected" + Environment.NewLine);
            rtbif100channel.Clear();
            rtbsps101channel.Clear();
            tbPortNumber.Clear();
            tbIPaddress.Clear();
            tbUsername.Clear();


        }

        // Event handler for the form's closing event. Sends a logout command to the server and closes the connection if connected.
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null && client.Connected)
            {
                try
                {
                    writer.WriteLine("LOGOUT");
                    writer.Flush();

                    
                    client.Close();
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show($"An error occurred while trying to close the connection: {ex.Message}");
                }
            }
        }

        // Event handler for the "Send Message" button click for the IF100 channel.
        // Sends the entered message to the server for the IF100 channel.
        private void btnsendmessageif100_Click(object sender, EventArgs e)
        {
            // Get the message from the TextBox.
            string message = tbMessageforif100.Text;
            // Check if the message is not empty.
            if (!string.IsNullOrEmpty(message))
            {
                // Call a method to send the message to the server for the IF100 channel.
                SendMessageToServer("IF100", message);

                tbMessageforif100.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Please enter a message to send.");
            }
        }

        // Sends a formatted message to the server for a specified channel.
        private void SendMessageToServer(string channel, string message)
        {
            // Format the message with the channel information.
            string formattedMessage = $"{channel}:{message}";

            if (client != null && client.Connected)
            {
                try
                {
                    // Send the formatted message to the server.
                    writer.WriteLine(formattedMessage);
                    writer.Flush();
                }
                catch (Exception ex)
                {
                    rtbstatus.AppendText($"Error sending message:  {ex.Message}\n");

                    MessageBox.Show($"Error sending message: {ex.Message}");
                }
            }
            else

            {
                rtbstatus.AppendText($"Not connected to the server. \n");

                MessageBox.Show("Not connected to the server.");
            }
        }

        // Displays a formatted message on the appropriate channel in the client's UI.
        private void DisplayMessage(string response)
        {
            // Split the received message into parts based on the colon (:).
            string[] parts = response.Split(new[] { ':' }, 4);
            // Check if the message has at least four parts (messageType, channel, senderOrSystem, messageContent).
            if (parts.Length >= 4)
            {
                // Extract individual parts from the split message.
                string messageType = parts[0];
                string channel = parts[1];
                string senderOrSystem = parts[2];
                string messageContent = parts[3];

                string displayText;

                if (messageType == "CONNECTION")
                {

                    // For system messages, do not append the username and use the system message directly.
                    displayText = $"[{DateTime.Now.ToString("HH:mm:ss")}] {senderOrSystem}: {messageContent}";
                }
                else
                {
                    // For normal messages, append the username and message content.
                    displayText = $"[{DateTime.Now.ToString("HH:mm:ss")}] {senderOrSystem}: {messageContent}";
                }

                this.Invoke((MethodInvoker)delegate
                {
                    // Display the message on the appropriate channel in the client's UI.
                    if (channel == "IF100")
                    {
                        rtbif100channel.AppendText(displayText + Environment.NewLine);
                    }
                    else if (channel == "SPS101")
                    {
                        rtbsps101channel.AppendText(displayText + Environment.NewLine);
                    }
                });
            }
        }

        // Starts a background task to continuously listen for messages from the server.
        private void StartListeningForMessages()
        {
            // Use Task.Run to run the listening task in the background.
            Task.Run(() =>
            {
                try
                {
                    // Continuously listen for messages while the client is connected.
                    while (client != null && client.Connected)
                    {
                        // Read a line from the server's response.
                        string response = reader.ReadLine();
                        if (!string.IsNullOrEmpty(response))
                        {
                            // Process the received server response.
                            ProcessServerResponse(response);
                        }
                    }
                }
                catch (IOException )
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        string errorMessage = $"Server connection was lost.";
                        MessageBox.Show(errorMessage);

                        if (client != null)
                        {
                            client.Close();
                            client = null;
                        }
                        ResetUI();
                    });
                }

            });
        }

        // Processes the response received from the server and updates the client's UI accordingly.
        private void ProcessServerResponse(string response)
        {
            // Use Invoke to update the UI elements from a different thread.
            this.Invoke((MethodInvoker)delegate
            {
                // Check the type of response and update the UI accordingly.
                if (response.StartsWith("MESSAGE"))
                {
                    // If the response is a regular message, display it in the appropriate channel.
                    DisplayMessage(response);
                }
                // Display a success message when the client's message is sent successfully.
                if (response == "YOUR_MESSAGE_SENT_SUCCESSFULLY")
                {
                    rtbstatus.AppendText("You have sent the message successfully!\n");
                }
                // If the response is a system message, display it in the appropriate channel.
                else if (response.StartsWith("CONNECTION"))
                {
                    DisplayMessage(response);
                }
                else if (response == "LOGOUT_SUCCESS")
                {
                    ResetUI();
                    // Reset the UI and close writer, reader, and client connections on successful logout.
                    if (writer != null)
                    {
                        writer.Close();
                        writer = null;
                    }
                    if (reader != null)
                    {
                        reader.Close();
                        reader = null;
                    }
                    client.Close();
                    rtbstatus.Clear();
                }
                else if (response.StartsWith("SUBSCRIBED:IF100"))
                {
                    // Update UI when successfully subscribed to the IF100 channel.
                    isSubscribedToIF100 = true;
                    UpdateButtonStates("IF100", true);
                    rtbstatus.AppendText("You have successfully subscribed to the IF100 channel! \n");
                    tbMessageforif100.Enabled = true;
                }
                else if (response.StartsWith("UNSUBSCRIBED:IF100"))
                {
                    // Update UI when successfully unsubscribed from the IF100 channel.
                    isSubscribedToIF100 = false;
                    UpdateButtonStates("IF100", false);
                    rtbstatus.AppendText("You have successfully unsubscribed to the IF100 channel! \n");
                    tbMessageforif100.Enabled = false;
                    rtbif100channel.Clear();

                }
                else if (response.StartsWith("SUBSCRIBED:SPS101"))
                {
                    // Update UI when successfully subscribed to the SPS101 channel.
                    isSubscribedToSPS101 = true;
                    UpdateButtonStates("SPS101", true);
                    rtbstatus.AppendText("You have successfully subscribed to the SPS101 channel! \n");
                    tbMessageforsps101.Enabled = true;

                }
                else if (response.StartsWith("UNSUBSCRIBED:SPS101"))
                {
                    // Update UI when successfully unsubscribed from the SPS101 channel.
                    isSubscribedToSPS101 = false;
                    UpdateButtonStates("SPS101", false);
                    rtbstatus.AppendText("You have successfully unsubscribed to the SPS101 channel! \n");
                    tbMessageforsps101.Enabled = false;
                    rtbsps101channel.Clear();
                }
                else if (response == "SERVER:DISCONNECT")
                {
                    // Display a message and close connections when the server requests a disconnect.
                    MessageBox.Show("Server has requested to disconnect.");
                    if (writer != null)
                    {
                        writer.Close();
                        writer = null;
                    }
                    if (reader != null)
                    {
                        reader.Close();
                        reader = null;
                    }
                    if (client != null)
                    {
                        client.Close();
                        client = null;
                    }
                    ResetUI();
                }
            });
        }

        // Event handler for the "Send Message to SPS101" button click.
        // Sends a message to the server for the SPS101 channel.
        private void btnsendmessagesps101_Click(object sender, EventArgs e)
        {
            // Get the message from the TextBox.
            string message = tbMessageforsps101.Text;
            // Check if the message is not empty.
            if (!string.IsNullOrEmpty(message))
            {
                // Call the SendMessageToServer function to send the message to the server for the SPS101 channel.
                SendMessageToServer("SPS101", message);

                tbMessageforsps101.Text = string.Empty;
            }
            else
            {
                // Display a message if the user tries to send an empty message.
                MessageBox.Show("Please enter a message to send.");
            }
        }
     
    }

}


