
namespace DiSUcordClient
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
            this.tbPortNumber = new System.Windows.Forms.TextBox();
            this.tbIPaddress = new System.Windows.Forms.TextBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbMessageforif100 = new System.Windows.Forms.TextBox();
            this.tbMessageforsps101 = new System.Windows.Forms.TextBox();
            this.rtbif100channel = new System.Windows.Forms.RichTextBox();
            this.rtbsps101channel = new System.Windows.Forms.RichTextBox();
            this.rtbstatus = new System.Windows.Forms.RichTextBox();
            this.btnlogin = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnsubscribeif100 = new System.Windows.Forms.Button();
            this.btnsubscribesps101 = new System.Windows.Forms.Button();
            this.btnunsubscribeif100 = new System.Windows.Forms.Button();
            this.btnunsubscribesps101 = new System.Windows.Forms.Button();
            this.btnsendmessageif100 = new System.Windows.Forms.Button();
            this.btnsendmessagesps101 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port Number: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "IP Address:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Username: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 263);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Connection Status: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(373, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "IF100 Channel";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(922, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "SPS101 Channel";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(373, 596);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Message:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(933, 596);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 20);
            this.label8.TabIndex = 7;
            this.label8.Text = "Message:";
            // 
            // tbPortNumber
            // 
            this.tbPortNumber.Location = new System.Drawing.Point(136, 60);
            this.tbPortNumber.Name = "tbPortNumber";
            this.tbPortNumber.Size = new System.Drawing.Size(160, 26);
            this.tbPortNumber.TabIndex = 8;
            // 
            // tbIPaddress
            // 
            this.tbIPaddress.Location = new System.Drawing.Point(136, 103);
            this.tbIPaddress.Name = "tbIPaddress";
            this.tbIPaddress.Size = new System.Drawing.Size(160, 26);
            this.tbIPaddress.TabIndex = 9;
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(136, 149);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(160, 26);
            this.tbUsername.TabIndex = 10;
            // 
            // tbMessageforif100
            // 
            this.tbMessageforif100.Location = new System.Drawing.Point(377, 619);
            this.tbMessageforif100.Name = "tbMessageforif100";
            this.tbMessageforif100.Size = new System.Drawing.Size(263, 26);
            this.tbMessageforif100.TabIndex = 11;
            // 
            // tbMessageforsps101
            // 
            this.tbMessageforsps101.Location = new System.Drawing.Point(937, 619);
            this.tbMessageforsps101.Name = "tbMessageforsps101";
            this.tbMessageforsps101.Size = new System.Drawing.Size(276, 26);
            this.tbMessageforsps101.TabIndex = 12;
            // 
            // rtbif100channel
            // 
            this.rtbif100channel.Location = new System.Drawing.Point(377, 35);
            this.rtbif100channel.Name = "rtbif100channel";
            this.rtbif100channel.Size = new System.Drawing.Size(401, 485);
            this.rtbif100channel.TabIndex = 13;
            this.rtbif100channel.Text = "";
            // 
            // rtbsps101channel
            // 
            this.rtbsps101channel.Location = new System.Drawing.Point(926, 35);
            this.rtbsps101channel.Name = "rtbsps101channel";
            this.rtbsps101channel.Size = new System.Drawing.Size(412, 485);
            this.rtbsps101channel.TabIndex = 14;
            this.rtbsps101channel.Text = "";
            // 
            // rtbstatus
            // 
            this.rtbstatus.Location = new System.Drawing.Point(12, 286);
            this.rtbstatus.Name = "rtbstatus";
            this.rtbstatus.Size = new System.Drawing.Size(333, 359);
            this.rtbstatus.TabIndex = 15;
            this.rtbstatus.Text = "";
            // 
            // btnlogin
            // 
            this.btnlogin.Location = new System.Drawing.Point(28, 204);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(75, 39);
            this.btnlogin.TabIndex = 16;
            this.btnlogin.Text = "Login";
            this.btnlogin.UseVisualStyleBackColor = true;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(120, 204);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 39);
            this.btnLogout.TabIndex = 17;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnsubscribeif100
            // 
            this.btnsubscribeif100.Location = new System.Drawing.Point(377, 526);
            this.btnsubscribeif100.Name = "btnsubscribeif100";
            this.btnsubscribeif100.Size = new System.Drawing.Size(125, 39);
            this.btnsubscribeif100.TabIndex = 18;
            this.btnsubscribeif100.Text = "Subscribe";
            this.btnsubscribeif100.UseVisualStyleBackColor = true;
            this.btnsubscribeif100.Click += new System.EventHandler(this.btnsubscribeif100_Click);
            // 
            // btnsubscribesps101
            // 
            this.btnsubscribesps101.Location = new System.Drawing.Point(926, 526);
            this.btnsubscribesps101.Name = "btnsubscribesps101";
            this.btnsubscribesps101.Size = new System.Drawing.Size(125, 39);
            this.btnsubscribesps101.TabIndex = 19;
            this.btnsubscribesps101.Text = "Subscribe";
            this.btnsubscribesps101.UseVisualStyleBackColor = true;
            this.btnsubscribesps101.Click += new System.EventHandler(this.btnsubscribesps101_Click);
            // 
            // btnunsubscribeif100
            // 
            this.btnunsubscribeif100.Location = new System.Drawing.Point(515, 526);
            this.btnunsubscribeif100.Name = "btnunsubscribeif100";
            this.btnunsubscribeif100.Size = new System.Drawing.Size(125, 39);
            this.btnunsubscribeif100.TabIndex = 20;
            this.btnunsubscribeif100.Text = "Unsubscribe";
            this.btnunsubscribeif100.UseVisualStyleBackColor = true;
            this.btnunsubscribeif100.Click += new System.EventHandler(this.btnunsubscribeif100_Click);
            // 
            // btnunsubscribesps101
            // 
            this.btnunsubscribesps101.Location = new System.Drawing.Point(1057, 526);
            this.btnunsubscribesps101.Name = "btnunsubscribesps101";
            this.btnunsubscribesps101.Size = new System.Drawing.Size(125, 39);
            this.btnunsubscribesps101.TabIndex = 21;
            this.btnunsubscribesps101.Text = "Unsubscribe";
            this.btnunsubscribesps101.UseVisualStyleBackColor = true;
            this.btnunsubscribesps101.Click += new System.EventHandler(this.btnunsubscribesps101_Click);
            // 
            // btnsendmessageif100
            // 
            this.btnsendmessageif100.Location = new System.Drawing.Point(703, 606);
            this.btnsendmessageif100.Name = "btnsendmessageif100";
            this.btnsendmessageif100.Size = new System.Drawing.Size(75, 39);
            this.btnsendmessageif100.TabIndex = 22;
            this.btnsendmessageif100.Text = "Send";
            this.btnsendmessageif100.UseVisualStyleBackColor = true;
            this.btnsendmessageif100.Click += new System.EventHandler(this.btnsendmessageif100_Click);
            // 
            // btnsendmessagesps101
            // 
            this.btnsendmessagesps101.Location = new System.Drawing.Point(1263, 606);
            this.btnsendmessagesps101.Name = "btnsendmessagesps101";
            this.btnsendmessagesps101.Size = new System.Drawing.Size(75, 39);
            this.btnsendmessagesps101.TabIndex = 23;
            this.btnsendmessagesps101.Text = "Send";
            this.btnsendmessagesps101.UseVisualStyleBackColor = true;
            this.btnsendmessagesps101.Click += new System.EventHandler(this.btnsendmessagesps101_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1407, 671);
            this.Controls.Add(this.btnsendmessagesps101);
            this.Controls.Add(this.btnsendmessageif100);
            this.Controls.Add(this.btnunsubscribesps101);
            this.Controls.Add(this.btnunsubscribeif100);
            this.Controls.Add(this.btnsubscribesps101);
            this.Controls.Add(this.btnsubscribeif100);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnlogin);
            this.Controls.Add(this.rtbstatus);
            this.Controls.Add(this.rtbsps101channel);
            this.Controls.Add(this.rtbif100channel);
            this.Controls.Add(this.tbMessageforsps101);
            this.Controls.Add(this.tbMessageforif100);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.tbIPaddress);
            this.Controls.Add(this.tbPortNumber);
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
        private System.Windows.Forms.TextBox tbPortNumber;
        private System.Windows.Forms.TextBox tbIPaddress;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbMessageforif100;
        private System.Windows.Forms.TextBox tbMessageforsps101;
        private System.Windows.Forms.RichTextBox rtbif100channel;
        private System.Windows.Forms.RichTextBox rtbsps101channel;
        private System.Windows.Forms.RichTextBox rtbstatus;
        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnsubscribeif100;
        private System.Windows.Forms.Button btnsubscribesps101;
        private System.Windows.Forms.Button btnunsubscribeif100;
        private System.Windows.Forms.Button btnunsubscribesps101;
        private System.Windows.Forms.Button btnsendmessageif100;
        private System.Windows.Forms.Button btnsendmessagesps101;
    }
}

