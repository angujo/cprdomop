namespace WindowsFormsAppTest
{
    partial class SettingsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbService = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDBPath = new System.Windows.Forms.Button();
            this.tbDBPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbPgsql = new System.Windows.Forms.GroupBox();
            this.tbDBPassword = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbDBUsername = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbDBSchema = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbDBName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbDBPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDBServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rbPostgreSQL = new System.Windows.Forms.RadioButton();
            this.rbSQLite = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.llReset = new System.Windows.Forms.LinkLabel();
            this.btnLogPath = new System.Windows.Forms.Button();
            this.tbLogPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPgSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbPgsql.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tbService);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.gbPgsql);
            this.groupBox1.Controls.Add(this.rbPostgreSQL);
            this.groupBox1.Controls.Add(this.rbSQLite);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.llReset);
            this.groupBox1.Controls.Add(this.btnLogPath);
            this.groupBox1.Controls.Add(this.tbLogPath);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(436, 315);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application Configuraion Locations";
            // 
            // tbService
            // 
            this.tbService.Location = new System.Drawing.Point(129, 19);
            this.tbService.Name = "tbService";
            this.tbService.Size = new System.Drawing.Size(293, 20);
            this.tbService.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Service Name";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDBPath);
            this.groupBox3.Controls.Add(this.tbDBPath);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(12, 229);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(409, 50);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SQLite Connection";
            // 
            // btnDBPath
            // 
            this.btnDBPath.FlatAppearance.BorderSize = 0;
            this.btnDBPath.Location = new System.Drawing.Point(374, 18);
            this.btnDBPath.Name = "btnDBPath";
            this.btnDBPath.Size = new System.Drawing.Size(29, 21);
            this.btnDBPath.TabIndex = 5;
            this.btnDBPath.Text = "...";
            this.btnDBPath.UseVisualStyleBackColor = true;
            // 
            // tbDBPath
            // 
            this.tbDBPath.Location = new System.Drawing.Point(126, 19);
            this.tbDBPath.Name = "tbDBPath";
            this.tbDBPath.ReadOnly = true;
            this.tbDBPath.Size = new System.Drawing.Size(242, 20);
            this.tbDBPath.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Database File Location";
            // 
            // gbPgsql
            // 
            this.gbPgsql.Controls.Add(this.btnPgSave);
            this.gbPgsql.Controls.Add(this.tbDBPassword);
            this.gbPgsql.Controls.Add(this.label8);
            this.gbPgsql.Controls.Add(this.tbDBUsername);
            this.gbPgsql.Controls.Add(this.label9);
            this.gbPgsql.Controls.Add(this.tbDBSchema);
            this.gbPgsql.Controls.Add(this.label7);
            this.gbPgsql.Controls.Add(this.tbDBName);
            this.gbPgsql.Controls.Add(this.label6);
            this.gbPgsql.Controls.Add(this.tbDBPort);
            this.gbPgsql.Controls.Add(this.label5);
            this.gbPgsql.Controls.Add(this.tbDBServer);
            this.gbPgsql.Controls.Add(this.label4);
            this.gbPgsql.Location = new System.Drawing.Point(13, 98);
            this.gbPgsql.Name = "gbPgsql";
            this.gbPgsql.Size = new System.Drawing.Size(409, 125);
            this.gbPgsql.TabIndex = 10;
            this.gbPgsql.TabStop = false;
            this.gbPgsql.Text = "PostgreSQL Connection";
            // 
            // tbDBPassword
            // 
            this.tbDBPassword.Location = new System.Drawing.Point(277, 68);
            this.tbDBPassword.Name = "tbDBPassword";
            this.tbDBPassword.PasswordChar = '*';
            this.tbDBPassword.Size = new System.Drawing.Size(124, 20);
            this.tbDBPassword.TabIndex = 22;
            this.tbDBPassword.TextChanged += new System.EventHandler(this.tbDBServer_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(225, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Password";
            // 
            // tbDBUsername
            // 
            this.tbDBUsername.Location = new System.Drawing.Point(63, 68);
            this.tbDBUsername.Name = "tbDBUsername";
            this.tbDBUsername.Size = new System.Drawing.Size(159, 20);
            this.tbDBUsername.TabIndex = 20;
            this.tbDBUsername.TextChanged += new System.EventHandler(this.tbDBServer_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Username";
            // 
            // tbDBSchema
            // 
            this.tbDBSchema.Location = new System.Drawing.Point(279, 42);
            this.tbDBSchema.Name = "tbDBSchema";
            this.tbDBSchema.Size = new System.Drawing.Size(124, 20);
            this.tbDBSchema.TabIndex = 18;
            this.tbDBSchema.TextChanged += new System.EventHandler(this.tbDBServer_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(227, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Schema";
            // 
            // tbDBName
            // 
            this.tbDBName.Location = new System.Drawing.Point(65, 42);
            this.tbDBName.Name = "tbDBName";
            this.tbDBName.Size = new System.Drawing.Size(159, 20);
            this.tbDBName.TabIndex = 16;
            this.tbDBName.TextChanged += new System.EventHandler(this.tbDBServer_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Database";
            // 
            // tbDBPort
            // 
            this.tbDBPort.Location = new System.Drawing.Point(338, 16);
            this.tbDBPort.Name = "tbDBPort";
            this.tbDBPort.Size = new System.Drawing.Size(65, 20);
            this.tbDBPort.TabIndex = 14;
            this.tbDBPort.TextChanged += new System.EventHandler(this.tbDBServer_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(306, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Port";
            // 
            // tbDBServer
            // 
            this.tbDBServer.Location = new System.Drawing.Point(63, 16);
            this.tbDBServer.Name = "tbDBServer";
            this.tbDBServer.Size = new System.Drawing.Size(237, 20);
            this.tbDBServer.TabIndex = 12;
            this.tbDBServer.TextChanged += new System.EventHandler(this.tbDBServer_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Server";
            // 
            // rbPostgreSQL
            // 
            this.rbPostgreSQL.AutoSize = true;
            this.rbPostgreSQL.Location = new System.Drawing.Point(221, 72);
            this.rbPostgreSQL.Name = "rbPostgreSQL";
            this.rbPostgreSQL.Size = new System.Drawing.Size(82, 17);
            this.rbPostgreSQL.TabIndex = 9;
            this.rbPostgreSQL.Tag = "postgres";
            this.rbPostgreSQL.Text = "PostgreSQL";
            this.rbPostgreSQL.UseVisualStyleBackColor = true;
            this.rbPostgreSQL.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // rbSQLite
            // 
            this.rbSQLite.AutoSize = true;
            this.rbSQLite.Location = new System.Drawing.Point(129, 72);
            this.rbSQLite.Name = "rbSQLite";
            this.rbSQLite.Size = new System.Drawing.Size(57, 17);
            this.rbSQLite.TabIndex = 8;
            this.rbSQLite.Tag = "sqlite";
            this.rbSQLite.Text = "SQLite";
            this.rbSQLite.UseVisualStyleBackColor = true;
            this.rbSQLite.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Schema Type";
            // 
            // llReset
            // 
            this.llReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llReset.AutoSize = true;
            this.llReset.Enabled = false;
            this.llReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llReset.Location = new System.Drawing.Point(7, 299);
            this.llReset.Name = "llReset";
            this.llReset.Size = new System.Drawing.Size(173, 13);
            this.llReset.TabIndex = 6;
            this.llReset.TabStop = true;
            this.llReset.Text = "Wish to reset the Paths? Click Here.";
            this.llReset.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llReset_LinkClicked);
            // 
            // btnLogPath
            // 
            this.btnLogPath.FlatAppearance.BorderSize = 0;
            this.btnLogPath.Location = new System.Drawing.Point(393, 42);
            this.btnLogPath.Name = "btnLogPath";
            this.btnLogPath.Size = new System.Drawing.Size(29, 21);
            this.btnLogPath.TabIndex = 5;
            this.btnLogPath.Text = "...";
            this.btnLogPath.UseVisualStyleBackColor = true;
            this.btnLogPath.Click += new System.EventHandler(this.btnLogPath_Click);
            // 
            // tbLogPath
            // 
            this.tbLogPath.Location = new System.Drawing.Point(129, 43);
            this.tbLogPath.Name = "tbLogPath";
            this.tbLogPath.ReadOnly = true;
            this.tbLogPath.Size = new System.Drawing.Size(263, 20);
            this.tbLogPath.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Log File Location";
            // 
            // btnPgSave
            // 
            this.btnPgSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPgSave.Location = new System.Drawing.Point(298, 94);
            this.btnPgSave.Name = "btnPgSave";
            this.btnPgSave.Size = new System.Drawing.Size(103, 23);
            this.btnPgSave.TabIndex = 23;
            this.btnPgSave.Text = "Test and Configure";
            this.btnPgSave.UseVisualStyleBackColor = true;
            this.btnPgSave.Click += new System.EventHandler(this.btnPgSave_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 339);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbPgsql.ResumeLayout(false);
            this.gbPgsql.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLogPath;
        private System.Windows.Forms.TextBox tbLogPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel llReset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbSQLite;
        private System.Windows.Forms.RadioButton rbPostgreSQL;
        private System.Windows.Forms.GroupBox gbPgsql;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDBPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDBServer;
        private System.Windows.Forms.TextBox tbDBName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbDBSchema;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbDBPassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbDBUsername;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDBPath;
        private System.Windows.Forms.TextBox tbDBPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbService;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnPgSave;
    }
}