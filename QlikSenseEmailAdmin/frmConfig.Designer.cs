namespace QlikSenseEmailAdmin
{
    partial class frmConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfig));
            this.button_Save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_qsurl = new System.Windows.Forms.TextBox();
            this.button_testurl = new System.Windows.Forms.Button();
            this.tb_server = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_port = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_username = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_pasword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_emailfrom = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_emailto = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_ssl = new System.Windows.Forms.CheckBox();
            this.tb_CustomProperty = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.button_email = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_wait = new System.Windows.Forms.TextBox();
            this.SetScheduler = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DeleteUsers = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.tb_interval = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_taskpassword = new System.Windows.Forms.TextBox();
            this.tb_taskuser = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.ResetSendHistory = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.ViewSendHistory = new System.Windows.Forms.Button();
            this.ViewLog = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(363, 607);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(161, 48);
            this.button_Save.TabIndex = 0;
            this.button_Save.Text = "SAVE SETTINGS";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "QlikSense Server URL:";
            // 
            // tb_qsurl
            // 
            this.tb_qsurl.Location = new System.Drawing.Point(133, 72);
            this.tb_qsurl.Name = "tb_qsurl";
            this.tb_qsurl.Size = new System.Drawing.Size(283, 20);
            this.tb_qsurl.TabIndex = 2;
            // 
            // button_testurl
            // 
            this.button_testurl.Location = new System.Drawing.Point(422, 69);
            this.button_testurl.Name = "button_testurl";
            this.button_testurl.Size = new System.Drawing.Size(103, 25);
            this.button_testurl.TabIndex = 3;
            this.button_testurl.Text = "Test Connection";
            this.button_testurl.UseVisualStyleBackColor = true;
            this.button_testurl.Click += new System.EventHandler(this.button_testurl_Click);
            // 
            // tb_server
            // 
            this.tb_server.Location = new System.Drawing.Point(114, 25);
            this.tb_server.Name = "tb_server";
            this.tb_server.Size = new System.Drawing.Size(283, 20);
            this.tb_server.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Server:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_port
            // 
            this.tb_port.Location = new System.Drawing.Point(114, 51);
            this.tb_port.Name = "tb_port";
            this.tb_port.Size = new System.Drawing.Size(67, 20);
            this.tb_port.TabIndex = 9;
            this.tb_port.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(79, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Port:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // tb_username
            // 
            this.tb_username.Location = new System.Drawing.Point(114, 78);
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(283, 20);
            this.tb_username.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "User Name:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_pasword
            // 
            this.tb_pasword.Location = new System.Drawing.Point(114, 104);
            this.tb_pasword.Name = "tb_pasword";
            this.tb_pasword.Size = new System.Drawing.Size(283, 20);
            this.tb_pasword.TabIndex = 13;
            this.tb_pasword.UseSystemPasswordChar = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Password:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_emailfrom
            // 
            this.tb_emailfrom.Location = new System.Drawing.Point(114, 130);
            this.tb_emailfrom.Name = "tb_emailfrom";
            this.tb_emailfrom.Size = new System.Drawing.Size(283, 20);
            this.tb_emailfrom.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "From Address:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_emailto
            // 
            this.tb_emailto.Location = new System.Drawing.Point(113, 103);
            this.tb_emailto.Name = "tb_emailto";
            this.tb_emailto.Size = new System.Drawing.Size(146, 20);
            this.tb_emailto.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "To Address:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_ssl);
            this.groupBox1.Controls.Add(this.tb_emailfrom);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tb_port);
            this.groupBox1.Controls.Add(this.tb_username);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tb_pasword);
            this.groupBox1.Controls.Add(this.tb_server);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(19, 124);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(509, 160);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Smtp Email Server Settings";
            // 
            // cb_ssl
            // 
            this.cb_ssl.AutoSize = true;
            this.cb_ssl.Location = new System.Drawing.Point(203, 51);
            this.cb_ssl.Name = "cb_ssl";
            this.cb_ssl.Size = new System.Drawing.Size(46, 17);
            this.cb_ssl.TabIndex = 0;
            this.cb_ssl.Text = "SSL";
            this.cb_ssl.UseVisualStyleBackColor = true;
            // 
            // tb_CustomProperty
            // 
            this.tb_CustomProperty.Location = new System.Drawing.Point(113, 51);
            this.tb_CustomProperty.Name = "tb_CustomProperty";
            this.tb_CustomProperty.Size = new System.Drawing.Size(117, 20);
            this.tb_CustomProperty.TabIndex = 21;
            this.tb_CustomProperty.Text = "TaskAlert";
            // 
            // label14
            // 
            this.label14.ForeColor = System.Drawing.Color.Green;
            this.label14.Location = new System.Drawing.Point(27, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(462, 26);
            this.label14.TabIndex = 20;
            this.label14.Text = "QMC Custom Property Name which contains email addresses for Notification ";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_email
            // 
            this.button_email.Location = new System.Drawing.Point(304, 96);
            this.button_email.Name = "button_email";
            this.button_email.Size = new System.Drawing.Size(92, 27);
            this.button_email.TabIndex = 19;
            this.button_email.Text = "Test Email";
            this.button_email.UseVisualStyleBackColor = true;
            this.button_email.Click += new System.EventHandler(this.button_email_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Wait:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_wait
            // 
            this.tb_wait.Location = new System.Drawing.Point(133, 98);
            this.tb_wait.Name = "tb_wait";
            this.tb_wait.Size = new System.Drawing.Size(67, 20);
            this.tb_wait.TabIndex = 5;
            // 
            // SetScheduler
            // 
            this.SetScheduler.Location = new System.Drawing.Point(324, 103);
            this.SetScheduler.Name = "SetScheduler";
            this.SetScheduler.Size = new System.Drawing.Size(161, 31);
            this.SetScheduler.TabIndex = 19;
            this.SetScheduler.Text = "Add/Update Scheduler";
            this.SetScheduler.UseVisualStyleBackColor = true;
            this.SetScheduler.Click += new System.EventHandler(this.SetScheduler_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DeleteUsers);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.tb_interval);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.tb_taskpassword);
            this.groupBox2.Controls.Add(this.tb_taskuser);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.SetScheduler);
            this.groupBox2.Location = new System.Drawing.Point(19, 438);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(509, 155);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Task Scheduler  ";
            // 
            // DeleteUsers
            // 
            this.DeleteUsers.Location = new System.Drawing.Point(414, 29);
            this.DeleteUsers.Name = "DeleteUsers";
            this.DeleteUsers.Size = new System.Drawing.Size(75, 23);
            this.DeleteUsers.TabIndex = 30;
            this.DeleteUsers.Text = "DeleteUsers";
            this.DeleteUsers.UseVisualStyleBackColor = true;
            this.DeleteUsers.Click += new System.EventHandler(this.DeleteUsers_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(232, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 31);
            this.button1.TabIndex = 26;
            this.button1.Text = "Remove";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(35, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Interval (Minutes):";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_interval
            // 
            this.tb_interval.Location = new System.Drawing.Point(132, 80);
            this.tb_interval.Name = "tb_interval";
            this.tb_interval.Size = new System.Drawing.Size(34, 20);
            this.tb_interval.TabIndex = 24;
            this.tb_interval.Text = "5";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Windows Password:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_taskpassword
            // 
            this.tb_taskpassword.Location = new System.Drawing.Point(132, 54);
            this.tb_taskpassword.Name = "tb_taskpassword";
            this.tb_taskpassword.Size = new System.Drawing.Size(215, 20);
            this.tb_taskpassword.TabIndex = 22;
            this.tb_taskpassword.UseSystemPasswordChar = true;
            // 
            // tb_taskuser
            // 
            this.tb_taskuser.Location = new System.Drawing.Point(132, 26);
            this.tb_taskuser.Name = "tb_taskuser";
            this.tb_taskuser.Size = new System.Drawing.Size(215, 20);
            this.tb_taskuser.TabIndex = 21;
            this.tb_taskuser.Text = "Domain\\User";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Windows UserName:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PictureBox1.BackgroundImage")));
            this.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("PictureBox1.InitialImage")));
            this.PictureBox1.Location = new System.Drawing.Point(136, 7);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(98, 60);
            this.PictureBox1.TabIndex = 21;
            this.PictureBox1.TabStop = false;
            // 
            // PictureBox2
            // 
            this.PictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PictureBox2.BackgroundImage")));
            this.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PictureBox2.InitialImage = ((System.Drawing.Image)(resources.GetObject("PictureBox2.InitialImage")));
            this.PictureBox2.Location = new System.Drawing.Point(-19, 0);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(225, 59);
            this.PictureBox2.TabIndex = 22;
            this.PictureBox2.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.ForestGreen;
            this.label12.Location = new System.Drawing.Point(98, 671);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(372, 26);
            this.label12.TabIndex = 23;
            this.label12.Text = "QlikSense Email Alert tool is not an official Qlik Product. Use at your own risk." +
    " \r\nCreated By Nick Akincilar && Marcus Spitzmiller   - Version 1.4c\r\n";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Location = new System.Drawing.Point(133, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 20);
            this.panel1.TabIndex = 24;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.ForestGreen;
            this.label13.Location = new System.Drawing.Point(253, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(272, 24);
            this.label13.TabIndex = 25;
            this.label13.Text = "QlikSense Email Alert Tool";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ResetSendHistory
            // 
            this.ResetSendHistory.Location = new System.Drawing.Point(19, 634);
            this.ResetSendHistory.Name = "ResetSendHistory";
            this.ResetSendHistory.Size = new System.Drawing.Size(114, 21);
            this.ResetSendHistory.TabIndex = 27;
            this.ResetSendHistory.Text = "Reset History";
            this.ResetSendHistory.UseVisualStyleBackColor = true;
            this.ResetSendHistory.Click += new System.EventHandler(this.ResetSendHistory_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(27, 54);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 13);
            this.label15.TabIndex = 22;
            this.label15.Text = "Property Name:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.ForeColor = System.Drawing.Color.Green;
            this.label16.Location = new System.Drawing.Point(27, 71);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(478, 26);
            this.label16.TabIndex = 23;
            this.label16.Text = "If a task is not associated with a custom property listed above then use the foll" +
    "owing address";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewSendHistory
            // 
            this.ViewSendHistory.Location = new System.Drawing.Point(19, 607);
            this.ViewSendHistory.Name = "ViewSendHistory";
            this.ViewSendHistory.Size = new System.Drawing.Size(114, 21);
            this.ViewSendHistory.TabIndex = 28;
            this.ViewSendHistory.Text = "View Send History";
            this.ViewSendHistory.UseVisualStyleBackColor = true;
            this.ViewSendHistory.Click += new System.EventHandler(this.ViewSendHistory_Click);
            // 
            // ViewLog
            // 
            this.ViewLog.Location = new System.Drawing.Point(146, 607);
            this.ViewLog.Name = "ViewLog";
            this.ViewLog.Size = new System.Drawing.Size(83, 48);
            this.ViewLog.TabIndex = 29;
            this.ViewLog.Text = "View Logs";
            this.ViewLog.UseVisualStyleBackColor = true;
            this.ViewLog.Click += new System.EventHandler(this.ViewLog_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.tb_emailto);
            this.groupBox3.Controls.Add(this.tb_CustomProperty);
            this.groupBox3.Controls.Add(this.button_email);
            this.groupBox3.Location = new System.Drawing.Point(19, 290);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(509, 142);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Where to Send Alerts?";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.OrangeRed;
            this.label17.Location = new System.Drawing.Point(333, 4);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(111, 13);
            this.label17.TabIndex = 30;
            this.label17.Text = "TLS 1.2";
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 717);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.ViewLog);
            this.Controls.Add(this.ViewSendHistory);
            this.Controls.Add(this.ResetSendHistory);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tb_qsurl);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.PictureBox2);
            this.Controls.Add(this.tb_wait);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_testurl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmConfig";
            this.Text = "QlikSense Email Alert Configuration";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_qsurl;
        private System.Windows.Forms.Button button_testurl;
        private System.Windows.Forms.TextBox tb_server;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_port;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_username;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_pasword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_emailfrom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_emailto;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cb_ssl;
        private System.Windows.Forms.Button button_email;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_wait;
        private System.Windows.Forms.Button SetScheduler;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_taskpassword;
        private System.Windows.Forms.TextBox tb_taskuser;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_interval;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.Windows.Forms.PictureBox PictureBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button ResetSendHistory;
        private System.Windows.Forms.TextBox tb_CustomProperty;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button ViewSendHistory;
        private System.Windows.Forms.Button ViewLog;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button DeleteUsers;
        private System.Windows.Forms.Label label17;
    }
}