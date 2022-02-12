namespace WindowsFormsAppTest
{
    partial class DBSchemaControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbSchemaHolder = new System.Windows.Forms.GroupBox();
            this.tbPort = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDatabase = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSchema = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.lbSave = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.gbSchemaHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPort)).BeginInit();
            this.SuspendLayout();
            // 
            // gbSchemaHolder
            // 
            this.gbSchemaHolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSchemaHolder.Controls.Add(this.label7);
            this.gbSchemaHolder.Controls.Add(this.lbSave);
            this.gbSchemaHolder.Controls.Add(this.btnTest);
            this.gbSchemaHolder.Controls.Add(this.tbPort);
            this.gbSchemaHolder.Controls.Add(this.label6);
            this.gbSchemaHolder.Controls.Add(this.pbProgress);
            this.gbSchemaHolder.Controls.Add(this.btnSave);
            this.gbSchemaHolder.Controls.Add(this.tbPassword);
            this.gbSchemaHolder.Controls.Add(this.label5);
            this.gbSchemaHolder.Controls.Add(this.tbUsername);
            this.gbSchemaHolder.Controls.Add(this.label4);
            this.gbSchemaHolder.Controls.Add(this.tbDatabase);
            this.gbSchemaHolder.Controls.Add(this.label3);
            this.gbSchemaHolder.Controls.Add(this.tbSchema);
            this.gbSchemaHolder.Controls.Add(this.label2);
            this.gbSchemaHolder.Controls.Add(this.tbServer);
            this.gbSchemaHolder.Controls.Add(this.label1);
            this.gbSchemaHolder.Location = new System.Drawing.Point(3, 3);
            this.gbSchemaHolder.Name = "gbSchemaHolder";
            this.gbSchemaHolder.Size = new System.Drawing.Size(626, 344);
            this.gbSchemaHolder.TabIndex = 0;
            this.gbSchemaHolder.TabStop = false;
            this.gbSchemaHolder.Text = "Schema Type";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(512, 35);
            this.tbPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(100, 26);
            this.tbPort.TabIndex = 13;
            this.tbPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbPort.Value = new decimal(new int[] {
            5132,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(468, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Port";
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(512, 282);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(99, 37);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(224, 222);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(388, 26);
            this.tbPassword.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Password";
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(224, 172);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(388, 26);
            this.tbUsername.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Username";
            // 
            // tbDatabase
            // 
            this.tbDatabase.Location = new System.Drawing.Point(224, 81);
            this.tbDatabase.Name = "tbDatabase";
            this.tbDatabase.Size = new System.Drawing.Size(388, 26);
            this.tbDatabase.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Database Name";
            // 
            // tbSchema
            // 
            this.tbSchema.Location = new System.Drawing.Point(224, 128);
            this.tbSchema.Name = "tbSchema";
            this.tbSchema.Size = new System.Drawing.Size(388, 26);
            this.tbSchema.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Schema Name";
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(224, 34);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(238, 26);
            this.tbServer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Name/IP Address";
            // 
            // btnTest
            // 
            this.btnTest.Enabled = false;
            this.btnTest.Location = new System.Drawing.Point(7, 282);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(149, 36);
            this.btnTest.TabIndex = 14;
            this.btnTest.Text = "Test Connection";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lbSave
            // 
            this.lbSave.AutoSize = true;
            this.lbSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSave.Location = new System.Drawing.Point(24, 264);
            this.lbSave.Name = "lbSave";
            this.lbSave.Size = new System.Drawing.Size(0, 15);
            this.lbSave.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Location = new System.Drawing.Point(6, 257);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(614, 2);
            this.label7.TabIndex = 16;
            this.label7.Text = "\r\n";
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(6, 323);
            this.pbProgress.MarqueeAnimationSpeed = 10;
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(605, 11);
            this.pbProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbProgress.TabIndex = 11;
            this.pbProgress.UseWaitCursor = true;
            this.pbProgress.Visible = false;
            // 
            // DBSchemaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbSchemaHolder);
            this.Name = "DBSchemaControl";
            this.Size = new System.Drawing.Size(632, 350);
            this.gbSchemaHolder.ResumeLayout(false);
            this.gbSchemaHolder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSchemaHolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDatabase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSchema;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown tbPort;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label lbSave;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar pbProgress;
    }
}
