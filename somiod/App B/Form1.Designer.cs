namespace App_B
{
    partial class Form1
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
            this.buttonOn = new System.Windows.Forms.Button();
            this.buttonOff = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.ipAddressTXT = new System.Windows.Forms.TextBox();
            this.connBTN = new System.Windows.Forms.Button();
            this.getTopicsBTN = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.appDrpDown = new System.Windows.Forms.ComboBox();
            this.ContainerDrpDown = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.app_discover = new System.Windows.Forms.Button();
            this.conVolume = new System.Windows.Forms.Button();
            this.conCanal = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxData = new System.Windows.Forms.ComboBox();
            this.comboBox_data = new System.Windows.Forms.Label();
            this.btn_discover_data = new System.Windows.Forms.Button();
            this.btn_delete_data = new System.Windows.Forms.Button();
            this.btn_delete_subscription = new System.Windows.Forms.Button();
            this.button_discoverSubscriptions = new System.Windows.Forms.Button();
            this.label_subscription = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOn
            // 
            this.buttonOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOn.Location = new System.Drawing.Point(27, 65);
            this.buttonOn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonOn.Name = "buttonOn";
            this.buttonOn.Size = new System.Drawing.Size(116, 58);
            this.buttonOn.TabIndex = 0;
            this.buttonOn.Text = "ON";
            this.buttonOn.UseVisualStyleBackColor = false;
            this.buttonOn.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonOff
            // 
            this.buttonOff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOff.Location = new System.Drawing.Point(181, 65);
            this.buttonOff.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonOff.Name = "buttonOff";
            this.buttonOff.Size = new System.Drawing.Size(116, 58);
            this.buttonOff.TabIndex = 1;
            this.buttonOff.Text = "OFF";
            this.buttonOff.UseVisualStyleBackColor = false;
            this.buttonOff.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SandyBrown;
            this.label1.Location = new System.Drawing.Point(61, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 38);
            this.label1.TabIndex = 2;
            this.label1.Text = "««Comando»»";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Volume";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(27, 170);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 22);
            this.numericUpDown1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Canal";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(27, 235);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 22);
            this.numericUpDown2.TabIndex = 6;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(515, 65);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Broker Domain";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // ipAddressTXT
            // 
            this.ipAddressTXT.Location = new System.Drawing.Point(520, 85);
            this.ipAddressTXT.Margin = new System.Windows.Forms.Padding(4);
            this.ipAddressTXT.Name = "ipAddressTXT";
            this.ipAddressTXT.Size = new System.Drawing.Size(132, 22);
            this.ipAddressTXT.TabIndex = 8;
            this.ipAddressTXT.Text = "127.0.0.1";
            // 
            // connBTN
            // 
            this.connBTN.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.connBTN.Location = new System.Drawing.Point(519, 117);
            this.connBTN.Margin = new System.Windows.Forms.Padding(4);
            this.connBTN.Name = "connBTN";
            this.connBTN.Size = new System.Drawing.Size(135, 28);
            this.connBTN.TabIndex = 9;
            this.connBTN.Text = "Connect";
            this.connBTN.UseVisualStyleBackColor = false;
            this.connBTN.Click += new System.EventHandler(this.connBTN_Click);
            // 
            // getTopicsBTN
            // 
            this.getTopicsBTN.Location = new System.Drawing.Point(227, 71);
            this.getTopicsBTN.Margin = new System.Windows.Forms.Padding(4);
            this.getTopicsBTN.Name = "getTopicsBTN";
            this.getTopicsBTN.Size = new System.Drawing.Size(160, 28);
            this.getTopicsBTN.TabIndex = 24;
            this.getTopicsBTN.Text = "Discover Containers";
            this.getTopicsBTN.UseVisualStyleBackColor = true;
            this.getTopicsBTN.Click += new System.EventHandler(this.getTopicsBTN_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 20);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 16);
            this.label6.TabIndex = 23;
            this.label6.Text = "Aplicações";
            // 
            // appDrpDown
            // 
            this.appDrpDown.FormattingEnabled = true;
            this.appDrpDown.Location = new System.Drawing.Point(21, 39);
            this.appDrpDown.Margin = new System.Windows.Forms.Padding(4);
            this.appDrpDown.Name = "appDrpDown";
            this.appDrpDown.Size = new System.Drawing.Size(160, 24);
            this.appDrpDown.TabIndex = 22;
            // 
            // ContainerDrpDown
            // 
            this.ContainerDrpDown.FormattingEnabled = true;
            this.ContainerDrpDown.Location = new System.Drawing.Point(227, 39);
            this.ContainerDrpDown.Margin = new System.Windows.Forms.Padding(4);
            this.ContainerDrpDown.Name = "ContainerDrpDown";
            this.ContainerDrpDown.Size = new System.Drawing.Size(160, 24);
            this.ContainerDrpDown.TabIndex = 19;
            this.ContainerDrpDown.SelectedIndexChanged += new System.EventHandler(this.TopicDrpDown_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(277, 20);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Containers";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.app_discover);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.ContainerDrpDown);
            this.groupBox1.Controls.Add(this.getTopicsBTN);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.appDrpDown);
            this.groupBox1.Location = new System.Drawing.Point(379, 153);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(420, 117);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Where to Publish";
            // 
            // app_discover
            // 
            this.app_discover.Location = new System.Drawing.Point(21, 73);
            this.app_discover.Name = "app_discover";
            this.app_discover.Size = new System.Drawing.Size(159, 25);
            this.app_discover.TabIndex = 25;
            this.app_discover.Text = "Discover Apps";
            this.app_discover.UseVisualStyleBackColor = true;
            this.app_discover.Click += new System.EventHandler(this.app_discover_Click);
            // 
            // conVolume
            // 
            this.conVolume.Location = new System.Drawing.Point(165, 166);
            this.conVolume.Margin = new System.Windows.Forms.Padding(4);
            this.conVolume.Name = "conVolume";
            this.conVolume.Size = new System.Drawing.Size(181, 28);
            this.conVolume.TabIndex = 26;
            this.conVolume.Text = "Confirmar Volume";
            this.conVolume.UseVisualStyleBackColor = true;
            this.conVolume.Click += new System.EventHandler(this.conVolume_Click);
            // 
            // conCanal
            // 
            this.conCanal.Location = new System.Drawing.Point(165, 231);
            this.conCanal.Margin = new System.Windows.Forms.Padding(4);
            this.conCanal.Name = "conCanal";
            this.conCanal.Size = new System.Drawing.Size(181, 28);
            this.conCanal.TabIndex = 27;
            this.conCanal.Text = "Confirmar Canal";
            this.conCanal.UseVisualStyleBackColor = true;
            this.conCanal.Click += new System.EventHandler(this.conCanal_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_delete_subscription);
            this.groupBox2.Controls.Add(this.button_discoverSubscriptions);
            this.groupBox2.Controls.Add(this.label_subscription);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.btn_delete_data);
            this.groupBox2.Controls.Add(this.btn_discover_data);
            this.groupBox2.Controls.Add(this.comboBox_data);
            this.groupBox2.Controls.Add(this.comboBoxData);
            this.groupBox2.Location = new System.Drawing.Point(218, 315);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(420, 152);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Discover";
            // 
            // comboBoxData
            // 
            this.comboBoxData.FormattingEnabled = true;
            this.comboBoxData.Location = new System.Drawing.Point(24, 41);
            this.comboBoxData.Name = "comboBoxData";
            this.comboBoxData.Size = new System.Drawing.Size(121, 24);
            this.comboBoxData.TabIndex = 0;
            // 
            // comboBox_data
            // 
            this.comboBox_data.AutoSize = true;
            this.comboBox_data.Location = new System.Drawing.Point(21, 22);
            this.comboBox_data.Name = "comboBox_data";
            this.comboBox_data.Size = new System.Drawing.Size(36, 16);
            this.comboBox_data.TabIndex = 1;
            this.comboBox_data.Text = "Data";
            // 
            // btn_discover_data
            // 
            this.btn_discover_data.Location = new System.Drawing.Point(24, 71);
            this.btn_discover_data.Name = "btn_discover_data";
            this.btn_discover_data.Size = new System.Drawing.Size(121, 23);
            this.btn_discover_data.TabIndex = 2;
            this.btn_discover_data.Text = "Discover Data";
            this.btn_discover_data.UseVisualStyleBackColor = true;
            // 
            // btn_delete_data
            // 
            this.btn_delete_data.Location = new System.Drawing.Point(24, 110);
            this.btn_delete_data.Name = "btn_delete_data";
            this.btn_delete_data.Size = new System.Drawing.Size(118, 27);
            this.btn_delete_data.TabIndex = 3;
            this.btn_delete_data.Text = "Delete Data";
            this.btn_delete_data.UseVisualStyleBackColor = true;
            // 
            // btn_delete_subscription
            // 
            this.btn_delete_subscription.Location = new System.Drawing.Point(190, 110);
            this.btn_delete_subscription.Name = "btn_delete_subscription";
            this.btn_delete_subscription.Size = new System.Drawing.Size(162, 27);
            this.btn_delete_subscription.TabIndex = 7;
            this.btn_delete_subscription.Text = "Delete Subscription";
            this.btn_delete_subscription.UseVisualStyleBackColor = true;
            // 
            // button_discoverSubscriptions
            // 
            this.button_discoverSubscriptions.Location = new System.Drawing.Point(190, 71);
            this.button_discoverSubscriptions.Name = "button_discoverSubscriptions";
            this.button_discoverSubscriptions.Size = new System.Drawing.Size(162, 23);
            this.button_discoverSubscriptions.TabIndex = 6;
            this.button_discoverSubscriptions.Text = "Discover Subscription";
            this.button_discoverSubscriptions.UseVisualStyleBackColor = true;
            // 
            // label_subscription
            // 
            this.label_subscription.AutoSize = true;
            this.label_subscription.Location = new System.Drawing.Point(187, 22);
            this.label_subscription.Name = "label_subscription";
            this.label_subscription.Size = new System.Drawing.Size(81, 16);
            this.label_subscription.TabIndex = 5;
            this.label_subscription.Text = "Subscription";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(190, 41);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(162, 24);
            this.comboBox1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 511);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.conCanal);
            this.Controls.Add(this.conVolume);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.connBTN);
            this.Controls.Add(this.ipAddressTXT);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOff);
            this.Controls.Add(this.buttonOn);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "App B";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOn;
        private System.Windows.Forms.Button buttonOff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ipAddressTXT;
        private System.Windows.Forms.Button connBTN;
        private System.Windows.Forms.Button getTopicsBTN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox appDrpDown;
        private System.Windows.Forms.ComboBox ContainerDrpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button conVolume;
        private System.Windows.Forms.Button conCanal;
        private System.Windows.Forms.Button app_discover;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label comboBox_data;
        private System.Windows.Forms.ComboBox comboBoxData;
        private System.Windows.Forms.Button btn_delete_subscription;
        private System.Windows.Forms.Button button_discoverSubscriptions;
        private System.Windows.Forms.Label label_subscription;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btn_delete_data;
        private System.Windows.Forms.Button btn_discover_data;
    }
}

