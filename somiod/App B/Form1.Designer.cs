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
            this.TopicDrpDown = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.conVolume = new System.Windows.Forms.Button();
            this.conCanal = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOn
            // 
            this.buttonOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOn.Location = new System.Drawing.Point(20, 53);
            this.buttonOn.Margin = new System.Windows.Forms.Padding(2);
            this.buttonOn.Name = "buttonOn";
            this.buttonOn.Size = new System.Drawing.Size(87, 47);
            this.buttonOn.TabIndex = 0;
            this.buttonOn.Text = "ON";
            this.buttonOn.UseVisualStyleBackColor = false;
            this.buttonOn.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonOff
            // 
            this.buttonOff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOff.Location = new System.Drawing.Point(136, 53);
            this.buttonOff.Margin = new System.Windows.Forms.Padding(2);
            this.buttonOff.Name = "buttonOff";
            this.buttonOff.Size = new System.Drawing.Size(87, 47);
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
            this.label1.Location = new System.Drawing.Point(46, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "««Comando»»";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 117);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Volume";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(20, 138);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(90, 20);
            this.numericUpDown1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 169);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Canal";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(20, 191);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(90, 20);
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
            this.label4.Location = new System.Drawing.Point(386, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Broker Domain";
            // 
            // ipAddressTXT
            // 
            this.ipAddressTXT.Location = new System.Drawing.Point(390, 69);
            this.ipAddressTXT.Name = "ipAddressTXT";
            this.ipAddressTXT.Size = new System.Drawing.Size(100, 20);
            this.ipAddressTXT.TabIndex = 8;
            this.ipAddressTXT.Text = "127.0.0.1";
            // 
            // connBTN
            // 
            this.connBTN.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.connBTN.Location = new System.Drawing.Point(389, 95);
            this.connBTN.Name = "connBTN";
            this.connBTN.Size = new System.Drawing.Size(101, 23);
            this.connBTN.TabIndex = 9;
            this.connBTN.Text = "Connect";
            this.connBTN.UseVisualStyleBackColor = false;
            this.connBTN.Click += new System.EventHandler(this.connBTN_Click);
            // 
            // getTopicsBTN
            // 
            this.getTopicsBTN.Location = new System.Drawing.Point(16, 59);
            this.getTopicsBTN.Name = "getTopicsBTN";
            this.getTopicsBTN.Size = new System.Drawing.Size(75, 23);
            this.getTopicsBTN.TabIndex = 24;
            this.getTopicsBTN.Text = "Get topics";
            this.getTopicsBTN.UseVisualStyleBackColor = true;
            this.getTopicsBTN.Click += new System.EventHandler(this.getTopicsBTN_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Aplicações";
            // 
            // appDrpDown
            // 
            this.appDrpDown.FormattingEnabled = true;
            this.appDrpDown.Location = new System.Drawing.Point(16, 32);
            this.appDrpDown.Name = "appDrpDown";
            this.appDrpDown.Size = new System.Drawing.Size(121, 21);
            this.appDrpDown.TabIndex = 22;
            // 
            // TopicDrpDown
            // 
            this.TopicDrpDown.FormattingEnabled = true;
            this.TopicDrpDown.Location = new System.Drawing.Point(170, 32);
            this.TopicDrpDown.Name = "TopicDrpDown";
            this.TopicDrpDown.Size = new System.Drawing.Size(121, 21);
            this.TopicDrpDown.TabIndex = 19;
            this.TopicDrpDown.SelectedIndexChanged += new System.EventHandler(this.TopicDrpDown_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(208, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Topicos";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.TopicDrpDown);
            this.groupBox1.Controls.Add(this.getTopicsBTN);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.appDrpDown);
            this.groupBox1.Location = new System.Drawing.Point(284, 124);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 95);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Where to Publish";
            // 
            // conVolume
            // 
            this.conVolume.Location = new System.Drawing.Point(124, 135);
            this.conVolume.Name = "conVolume";
            this.conVolume.Size = new System.Drawing.Size(136, 23);
            this.conVolume.TabIndex = 26;
            this.conVolume.Text = "Confirmar Volume";
            this.conVolume.UseVisualStyleBackColor = true;
            this.conVolume.Click += new System.EventHandler(this.conVolume_Click);
            // 
            // conCanal
            // 
            this.conCanal.Location = new System.Drawing.Point(124, 188);
            this.conCanal.Name = "conCanal";
            this.conCanal.Size = new System.Drawing.Size(136, 23);
            this.conCanal.TabIndex = 27;
            this.conCanal.Text = "Confirmar Canal";
            this.conCanal.UseVisualStyleBackColor = true;
            this.conCanal.Click += new System.EventHandler(this.conCanal_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 259);
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
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "App B";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.ComboBox TopicDrpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button conVolume;
        private System.Windows.Forms.Button conCanal;
    }
}

