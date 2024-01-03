namespace App_A
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
            this.label1 = new System.Windows.Forms.Label();
            this.labelEstado = new System.Windows.Forms.Label();
            this.estadoTXT = new System.Windows.Forms.TextBox();
            this.canalTXT = new System.Windows.Forms.TextBox();
            this.labelCanal = new System.Windows.Forms.Label();
            this.volumeTXT = new System.Windows.Forms.TextBox();
            this.labelVolume = new System.Windows.Forms.Label();
            this.ConnectBTN = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.IPAddresstxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TopicDrpDown = new System.Windows.Forms.ComboBox();
            this.SubBTN = new System.Windows.Forms.Button();
            this.UnsubBTN = new System.Windows.Forms.Button();
            this.appDrpDown = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.getTopicsBTN = new System.Windows.Forms.Button();
            this.createAppAndContainers = new System.Windows.Forms.Button();
            this.btn_delete_app_cont = new System.Windows.Forms.Button();
            this.button_disoverApps = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(75, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(316, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "**Televisão**";
            // 
            // labelEstado
            // 
            this.labelEstado.AutoSize = true;
            this.labelEstado.Location = new System.Drawing.Point(28, 112);
            this.labelEstado.Name = "labelEstado";
            this.labelEstado.Size = new System.Drawing.Size(50, 16);
            this.labelEstado.TabIndex = 1;
            this.labelEstado.Text = "Estado";
            // 
            // estadoTXT
            // 
            this.estadoTXT.Location = new System.Drawing.Point(84, 110);
            this.estadoTXT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.estadoTXT.Name = "estadoTXT";
            this.estadoTXT.ReadOnly = true;
            this.estadoTXT.Size = new System.Drawing.Size(100, 22);
            this.estadoTXT.TabIndex = 2;
            this.estadoTXT.TextChanged += new System.EventHandler(this.estadoTXT_TextChanged);
            // 
            // canalTXT
            // 
            this.canalTXT.Location = new System.Drawing.Point(84, 153);
            this.canalTXT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.canalTXT.Name = "canalTXT";
            this.canalTXT.ReadOnly = true;
            this.canalTXT.Size = new System.Drawing.Size(100, 22);
            this.canalTXT.TabIndex = 4;
            // 
            // labelCanal
            // 
            this.labelCanal.AutoSize = true;
            this.labelCanal.Location = new System.Drawing.Point(28, 155);
            this.labelCanal.Name = "labelCanal";
            this.labelCanal.Size = new System.Drawing.Size(42, 16);
            this.labelCanal.TabIndex = 3;
            this.labelCanal.Text = "Canal";
            // 
            // volumeTXT
            // 
            this.volumeTXT.Location = new System.Drawing.Point(84, 206);
            this.volumeTXT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.volumeTXT.Name = "volumeTXT";
            this.volumeTXT.ReadOnly = true;
            this.volumeTXT.Size = new System.Drawing.Size(100, 22);
            this.volumeTXT.TabIndex = 6;
            // 
            // labelVolume
            // 
            this.labelVolume.AutoSize = true;
            this.labelVolume.Location = new System.Drawing.Point(28, 209);
            this.labelVolume.Name = "labelVolume";
            this.labelVolume.Size = new System.Drawing.Size(53, 16);
            this.labelVolume.TabIndex = 5;
            this.labelVolume.Text = "Volume";
            // 
            // ConnectBTN
            // 
            this.ConnectBTN.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ConnectBTN.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ConnectBTN.Location = new System.Drawing.Point(291, 143);
            this.ConnectBTN.Margin = new System.Windows.Forms.Padding(4);
            this.ConnectBTN.Name = "ConnectBTN";
            this.ConnectBTN.Size = new System.Drawing.Size(117, 28);
            this.ConnectBTN.TabIndex = 8;
            this.ConnectBTN.Text = "Connect";
            this.ConnectBTN.UseVisualStyleBackColor = false;
            this.ConnectBTN.Click += new System.EventHandler(this.ConnectBTN_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(288, 95);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Broker Domain";
            // 
            // IPAddresstxt
            // 
            this.IPAddresstxt.Location = new System.Drawing.Point(292, 114);
            this.IPAddresstxt.Margin = new System.Windows.Forms.Padding(4);
            this.IPAddresstxt.Name = "IPAddresstxt";
            this.IPAddresstxt.Size = new System.Drawing.Size(132, 22);
            this.IPAddresstxt.TabIndex = 10;
            this.IPAddresstxt.Text = "127.0.0.1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(533, 238);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Containers";
            // 
            // TopicDrpDown
            // 
            this.TopicDrpDown.FormattingEnabled = true;
            this.TopicDrpDown.Location = new System.Drawing.Point(612, 235);
            this.TopicDrpDown.Margin = new System.Windows.Forms.Padding(4);
            this.TopicDrpDown.Name = "TopicDrpDown";
            this.TopicDrpDown.Size = new System.Drawing.Size(160, 24);
            this.TopicDrpDown.TabIndex = 12;
            // 
            // SubBTN
            // 
            this.SubBTN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.SubBTN.Location = new System.Drawing.Point(591, 267);
            this.SubBTN.Margin = new System.Windows.Forms.Padding(4);
            this.SubBTN.Name = "SubBTN";
            this.SubBTN.Size = new System.Drawing.Size(100, 28);
            this.SubBTN.TabIndex = 13;
            this.SubBTN.Text = "Subscribe";
            this.SubBTN.UseVisualStyleBackColor = false;
            this.SubBTN.Click += new System.EventHandler(this.SubBTN_Click);
            // 
            // UnsubBTN
            // 
            this.UnsubBTN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.UnsubBTN.Location = new System.Drawing.Point(699, 267);
            this.UnsubBTN.Margin = new System.Windows.Forms.Padding(4);
            this.UnsubBTN.Name = "UnsubBTN";
            this.UnsubBTN.Size = new System.Drawing.Size(100, 28);
            this.UnsubBTN.TabIndex = 14;
            this.UnsubBTN.Text = "Unsubscribe";
            this.UnsubBTN.UseVisualStyleBackColor = false;
            this.UnsubBTN.Click += new System.EventHandler(this.UnsubBTN_Click);
            // 
            // appDrpDown
            // 
            this.appDrpDown.FormattingEnabled = true;
            this.appDrpDown.Location = new System.Drawing.Point(612, 116);
            this.appDrpDown.Margin = new System.Windows.Forms.Padding(4);
            this.appDrpDown.Name = "appDrpDown";
            this.appDrpDown.Size = new System.Drawing.Size(160, 24);
            this.appDrpDown.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(609, 95);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Aplicações";
            // 
            // getTopicsBTN
            // 
            this.getTopicsBTN.Location = new System.Drawing.Point(612, 203);
            this.getTopicsBTN.Margin = new System.Windows.Forms.Padding(4);
            this.getTopicsBTN.Name = "getTopicsBTN";
            this.getTopicsBTN.Size = new System.Drawing.Size(165, 28);
            this.getTopicsBTN.TabIndex = 17;
            this.getTopicsBTN.Text = "Discover Containers";
            this.getTopicsBTN.UseVisualStyleBackColor = true;
            this.getTopicsBTN.Click += new System.EventHandler(this.getTopicsBTN_Click);
            // 
            // createAppAndContainers
            // 
            this.createAppAndContainers.Location = new System.Drawing.Point(571, 9);
            this.createAppAndContainers.Name = "createAppAndContainers";
            this.createAppAndContainers.Size = new System.Drawing.Size(104, 83);
            this.createAppAndContainers.TabIndex = 18;
            this.createAppAndContainers.Text = "Create App And Containers";
            this.createAppAndContainers.UseVisualStyleBackColor = true;
            this.createAppAndContainers.Click += new System.EventHandler(this.createAppAndContainers_Click);
            // 
            // btn_delete_app_cont
            // 
            this.btn_delete_app_cont.Location = new System.Drawing.Point(700, 12);
            this.btn_delete_app_cont.Name = "btn_delete_app_cont";
            this.btn_delete_app_cont.Size = new System.Drawing.Size(99, 80);
            this.btn_delete_app_cont.TabIndex = 19;
            this.btn_delete_app_cont.Text = "Delete App and Containers";
            this.btn_delete_app_cont.UseVisualStyleBackColor = true;
            this.btn_delete_app_cont.Click += new System.EventHandler(this.btn_delete_app_cont_Click);
            // 
            // button_disoverApps
            // 
            this.button_disoverApps.Location = new System.Drawing.Point(612, 143);
            this.button_disoverApps.Name = "button_disoverApps";
            this.button_disoverApps.Size = new System.Drawing.Size(160, 23);
            this.button_disoverApps.TabIndex = 20;
            this.button_disoverApps.Text = "Discover Apps";
            this.button_disoverApps.UseVisualStyleBackColor = true;
            this.button_disoverApps.Click += new System.EventHandler(this.button_disoverApps_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 333);
            this.Controls.Add(this.button_disoverApps);
            this.Controls.Add(this.btn_delete_app_cont);
            this.Controls.Add(this.createAppAndContainers);
            this.Controls.Add(this.getTopicsBTN);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.appDrpDown);
            this.Controls.Add(this.UnsubBTN);
            this.Controls.Add(this.SubBTN);
            this.Controls.Add(this.TopicDrpDown);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.IPAddresstxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ConnectBTN);
            this.Controls.Add(this.volumeTXT);
            this.Controls.Add(this.labelVolume);
            this.Controls.Add(this.canalTXT);
            this.Controls.Add(this.labelCanal);
            this.Controls.Add(this.estadoTXT);
            this.Controls.Add(this.labelEstado);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "App A";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelEstado;
        private System.Windows.Forms.TextBox estadoTXT;
        private System.Windows.Forms.TextBox canalTXT;
        private System.Windows.Forms.Label labelCanal;
        private System.Windows.Forms.TextBox volumeTXT;
        private System.Windows.Forms.Label labelVolume;
        private System.Windows.Forms.Button ConnectBTN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox IPAddresstxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox TopicDrpDown;
        private System.Windows.Forms.Button SubBTN;
        private System.Windows.Forms.Button UnsubBTN;
        private System.Windows.Forms.ComboBox appDrpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button getTopicsBTN;
        private System.Windows.Forms.Button createAppAndContainers;
        private System.Windows.Forms.Button btn_delete_app_cont;
        private System.Windows.Forms.Button button_disoverApps;
    }
}

