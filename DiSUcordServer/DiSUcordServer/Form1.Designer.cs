
namespace DiSUcordServer
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnconnect = new System.Windows.Forms.Button();
            this.btndisconnect = new System.Windows.Forms.Button();
            this.rtbcurrentlyconnectedclients = new System.Windows.Forms.RichTextBox();
            this.rtbif100subscribers = new System.Windows.Forms.RichTextBox();
            this.rtbsps101subscribers = new System.Windows.Forms.RichTextBox();
            this.rtbclientssubscribingchannels = new System.Windows.Forms.RichTextBox();
            this.rtbclientsunsubscribingchannels = new System.Windows.Forms.RichTextBox();
            this.rtbsendingmessages = new System.Windows.Forms.RichTextBox();
            this.rtbdisconnections = new System.Windows.Forms.RichTextBox();
            this.rtbunsuccesfulattempts = new System.Windows.Forms.RichTextBox();
            this.rtbserveractivity = new System.Windows.Forms.RichTextBox();
            this.tbportnumber = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port Number: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Currently Connected Clients";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(246, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "IF100 Subscribers";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(472, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "SPS101 Subscribers";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(708, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(215, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Clients Subscribing Channels";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1028, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(233, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Clients Unsubscribing Channels";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 594);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Sending Messages";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(391, 594);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 20);
            this.label8.TabIndex = 7;
            this.label8.Text = "Disconnections";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(708, 594);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(166, 20);
            this.label9.TabIndex = 8;
            this.label9.Text = "Unsuccesful Attempts";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1028, 594);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 20);
            this.label10.TabIndex = 9;
            this.label10.Text = "Server Activity";
            // 
            // btnconnect
            // 
            this.btnconnect.Location = new System.Drawing.Point(378, 29);
            this.btnconnect.Name = "btnconnect";
            this.btnconnect.Size = new System.Drawing.Size(117, 33);
            this.btnconnect.TabIndex = 10;
            this.btnconnect.Text = "Connect";
            this.btnconnect.UseVisualStyleBackColor = true;
            this.btnconnect.Click += new System.EventHandler(this.btnconnect_Click);
            // 
            // btndisconnect
            // 
            this.btndisconnect.Location = new System.Drawing.Point(501, 29);
            this.btndisconnect.Name = "btndisconnect";
            this.btndisconnect.Size = new System.Drawing.Size(117, 33);
            this.btndisconnect.TabIndex = 11;
            this.btndisconnect.Text = "Disconnect";
            this.btndisconnect.UseVisualStyleBackColor = true;
            this.btndisconnect.Click += new System.EventHandler(this.btndisconnect_Click);
            // 
            // rtbcurrentlyconnectedclients
            // 
            this.rtbcurrentlyconnectedclients.Location = new System.Drawing.Point(12, 110);
            this.rtbcurrentlyconnectedclients.Name = "rtbcurrentlyconnectedclients";
            this.rtbcurrentlyconnectedclients.Size = new System.Drawing.Size(220, 467);
            this.rtbcurrentlyconnectedclients.TabIndex = 12;
            this.rtbcurrentlyconnectedclients.Text = "";
            // 
            // rtbif100subscribers
            // 
            this.rtbif100subscribers.Location = new System.Drawing.Point(238, 110);
            this.rtbif100subscribers.Name = "rtbif100subscribers";
            this.rtbif100subscribers.Size = new System.Drawing.Size(228, 467);
            this.rtbif100subscribers.TabIndex = 13;
            this.rtbif100subscribers.Text = "";
            // 
            // rtbsps101subscribers
            // 
            this.rtbsps101subscribers.Location = new System.Drawing.Point(472, 110);
            this.rtbsps101subscribers.Name = "rtbsps101subscribers";
            this.rtbsps101subscribers.Size = new System.Drawing.Size(234, 467);
            this.rtbsps101subscribers.TabIndex = 14;
            this.rtbsps101subscribers.Text = "";
            // 
            // rtbclientssubscribingchannels
            // 
            this.rtbclientssubscribingchannels.Location = new System.Drawing.Point(716, 110);
            this.rtbclientssubscribingchannels.Name = "rtbclientssubscribingchannels";
            this.rtbclientssubscribingchannels.Size = new System.Drawing.Size(310, 467);
            this.rtbclientssubscribingchannels.TabIndex = 15;
            this.rtbclientssubscribingchannels.Text = "";
            // 
            // rtbclientsunsubscribingchannels
            // 
            this.rtbclientsunsubscribingchannels.Location = new System.Drawing.Point(1032, 110);
            this.rtbclientsunsubscribingchannels.Name = "rtbclientsunsubscribingchannels";
            this.rtbclientsunsubscribingchannels.Size = new System.Drawing.Size(334, 467);
            this.rtbclientsunsubscribingchannels.TabIndex = 16;
            this.rtbclientsunsubscribingchannels.Text = "";
            // 
            // rtbsendingmessages
            // 
            this.rtbsendingmessages.Location = new System.Drawing.Point(12, 617);
            this.rtbsendingmessages.Name = "rtbsendingmessages";
            this.rtbsendingmessages.Size = new System.Drawing.Size(373, 406);
            this.rtbsendingmessages.TabIndex = 17;
            this.rtbsendingmessages.Text = "";
            // 
            // rtbdisconnections
            // 
            this.rtbdisconnections.Location = new System.Drawing.Point(395, 617);
            this.rtbdisconnections.Name = "rtbdisconnections";
            this.rtbdisconnections.Size = new System.Drawing.Size(311, 406);
            this.rtbdisconnections.TabIndex = 18;
            this.rtbdisconnections.Text = "";
            // 
            // rtbunsuccesfulattempts
            // 
            this.rtbunsuccesfulattempts.Location = new System.Drawing.Point(712, 617);
            this.rtbunsuccesfulattempts.Name = "rtbunsuccesfulattempts";
            this.rtbunsuccesfulattempts.Size = new System.Drawing.Size(314, 406);
            this.rtbunsuccesfulattempts.TabIndex = 19;
            this.rtbunsuccesfulattempts.Text = "";
            // 
            // rtbserveractivity
            // 
            this.rtbserveractivity.Location = new System.Drawing.Point(1032, 617);
            this.rtbserveractivity.Name = "rtbserveractivity";
            this.rtbserveractivity.Size = new System.Drawing.Size(334, 406);
            this.rtbserveractivity.TabIndex = 20;
            this.rtbserveractivity.Text = "";
            // 
            // tbportnumber
            // 
            this.tbportnumber.Location = new System.Drawing.Point(148, 32);
            this.tbportnumber.Name = "tbportnumber";
            this.tbportnumber.Size = new System.Drawing.Size(208, 26);
            this.tbportnumber.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1378, 1047);
            this.Controls.Add(this.tbportnumber);
            this.Controls.Add(this.rtbserveractivity);
            this.Controls.Add(this.rtbunsuccesfulattempts);
            this.Controls.Add(this.rtbdisconnections);
            this.Controls.Add(this.rtbsendingmessages);
            this.Controls.Add(this.rtbclientsunsubscribingchannels);
            this.Controls.Add(this.rtbclientssubscribingchannels);
            this.Controls.Add(this.rtbsps101subscribers);
            this.Controls.Add(this.rtbif100subscribers);
            this.Controls.Add(this.rtbcurrentlyconnectedclients);
            this.Controls.Add(this.btndisconnect);
            this.Controls.Add(this.btnconnect);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnconnect;
        private System.Windows.Forms.Button btndisconnect;
        private System.Windows.Forms.RichTextBox rtbcurrentlyconnectedclients;
        private System.Windows.Forms.RichTextBox rtbif100subscribers;
        private System.Windows.Forms.RichTextBox rtbsps101subscribers;
        private System.Windows.Forms.RichTextBox rtbclientssubscribingchannels;
        private System.Windows.Forms.RichTextBox rtbclientsunsubscribingchannels;
        private System.Windows.Forms.RichTextBox rtbsendingmessages;
        private System.Windows.Forms.RichTextBox rtbdisconnections;
        private System.Windows.Forms.RichTextBox rtbunsuccesfulattempts;
        private System.Windows.Forms.RichTextBox rtbserveractivity;
        private System.Windows.Forms.TextBox tbportnumber;
    }
}

