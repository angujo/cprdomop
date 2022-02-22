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
            this.btnLogPath = new System.Windows.Forms.Button();
            this.tbLogPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDBPath = new System.Windows.Forms.Button();
            this.tbDBPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.llReset = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.llReset);
            this.groupBox1.Controls.Add(this.btnLogPath);
            this.groupBox1.Controls.Add(this.tbLogPath);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnDBPath);
            this.groupBox1.Controls.Add(this.tbDBPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(436, 134);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Application Configuraion Locations";
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
            // btnDBPath
            // 
            this.btnDBPath.FlatAppearance.BorderSize = 0;
            this.btnDBPath.Location = new System.Drawing.Point(393, 16);
            this.btnDBPath.Name = "btnDBPath";
            this.btnDBPath.Size = new System.Drawing.Size(29, 21);
            this.btnDBPath.TabIndex = 2;
            this.btnDBPath.Text = "...";
            this.btnDBPath.UseVisualStyleBackColor = true;
            this.btnDBPath.Click += new System.EventHandler(this.btnDBPath_Click);
            // 
            // tbDBPath
            // 
            this.tbDBPath.Location = new System.Drawing.Point(129, 17);
            this.tbDBPath.Name = "tbDBPath";
            this.tbDBPath.ReadOnly = true;
            this.tbDBPath.Size = new System.Drawing.Size(263, 20);
            this.tbDBPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Database File Location";
            // 
            // llReset
            // 
            this.llReset.AutoSize = true;
            this.llReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llReset.Location = new System.Drawing.Point(7, 118);
            this.llReset.Name = "llReset";
            this.llReset.Size = new System.Drawing.Size(173, 13);
            this.llReset.TabIndex = 6;
            this.llReset.TabStop = true;
            this.llReset.Text = "Wish to reset the Paths? Click Here.";
            this.llReset.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llReset_LinkClicked);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 158);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDBPath;
        private System.Windows.Forms.Button btnDBPath;
        private System.Windows.Forms.Button btnLogPath;
        private System.Windows.Forms.TextBox tbLogPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel llReset;
    }
}