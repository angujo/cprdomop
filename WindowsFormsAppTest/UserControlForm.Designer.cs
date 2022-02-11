namespace WindowsFormsAppTest
{
    partial class UserControlForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSummary = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.tabConf = new System.Windows.Forms.TabPage();
            this.pnConf = new System.Windows.Forms.Panel();
            this.tabSource = new System.Windows.Forms.TabPage();
            this.tabCdm = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.tabRuntime = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.loadTitle = new System.Windows.Forms.Label();
            this.pnDB = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabSummary.SuspendLayout();
            this.tabConf.SuspendLayout();
            this.tabSource.SuspendLayout();
            this.tabCdm.SuspendLayout();
            this.tabRuntime.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabSummary);
            this.tabControl1.Controls.Add(this.tabConf);
            this.tabControl1.Controls.Add(this.tabSource);
            this.tabControl1.Controls.Add(this.tabCdm);
            this.tabControl1.Controls.Add(this.tabRuntime);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 136);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(10, 5);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1157, 559);
            this.tabControl1.TabIndex = 1;
            // 
            // tabSummary
            // 
            this.tabSummary.BackColor = System.Drawing.Color.MistyRose;
            this.tabSummary.Controls.Add(this.label2);
            this.tabSummary.Location = new System.Drawing.Point(4, 38);
            this.tabSummary.Name = "tabSummary";
            this.tabSummary.Padding = new System.Windows.Forms.Padding(3);
            this.tabSummary.Size = new System.Drawing.Size(1149, 517);
            this.tabSummary.TabIndex = 0;
            this.tabSummary.Text = "Summary";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(477, 285);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "For Tab 101";
            // 
            // tabConf
            // 
            this.tabConf.Controls.Add(this.pnConf);
            this.tabConf.Location = new System.Drawing.Point(4, 38);
            this.tabConf.Name = "tabConf";
            this.tabConf.Padding = new System.Windows.Forms.Padding(3);
            this.tabConf.Size = new System.Drawing.Size(1149, 517);
            this.tabConf.TabIndex = 1;
            this.tabConf.Text = "Configuration";
            this.tabConf.UseVisualStyleBackColor = true;
            // 
            // pnConf
            // 
            this.pnConf.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnConf.AutoScroll = true;
            this.pnConf.Location = new System.Drawing.Point(27, 29);
            this.pnConf.Name = "pnConf";
            this.pnConf.Size = new System.Drawing.Size(1085, 463);
            this.pnConf.TabIndex = 0;
            // 
            // tabSource
            // 
            this.tabSource.BackColor = System.Drawing.SystemColors.Window;
            this.tabSource.Controls.Add(this.pnDB);
            this.tabSource.Location = new System.Drawing.Point(4, 38);
            this.tabSource.Name = "tabSource";
            this.tabSource.Padding = new System.Windows.Forms.Padding(3);
            this.tabSource.Size = new System.Drawing.Size(1149, 517);
            this.tabSource.TabIndex = 2;
            this.tabSource.Text = "Source";
            // 
            // tabCdm
            // 
            this.tabCdm.BackColor = System.Drawing.Color.MistyRose;
            this.tabCdm.Controls.Add(this.label3);
            this.tabCdm.Location = new System.Drawing.Point(4, 38);
            this.tabCdm.Name = "tabCdm";
            this.tabCdm.Padding = new System.Windows.Forms.Padding(3);
            this.tabCdm.Size = new System.Drawing.Size(1149, 517);
            this.tabCdm.TabIndex = 3;
            this.tabCdm.Text = "OMOP CDM";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(477, 285);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "For Tab 101";
            // 
            // tabRuntime
            // 
            this.tabRuntime.BackColor = System.Drawing.Color.MistyRose;
            this.tabRuntime.Controls.Add(this.label5);
            this.tabRuntime.Location = new System.Drawing.Point(4, 38);
            this.tabRuntime.Name = "tabRuntime";
            this.tabRuntime.Padding = new System.Windows.Forms.Padding(3);
            this.tabRuntime.Size = new System.Drawing.Size(1149, 517);
            this.tabRuntime.TabIndex = 4;
            this.tabRuntime.Text = "Runtime";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(477, 285);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 25);
            this.label5.TabIndex = 0;
            this.label5.Text = "For Tab 101";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.Controls.Add(this.loadTitle);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(4, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1153, 90);
            this.panel1.TabIndex = 2;
            // 
            // loadTitle
            // 
            this.loadTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loadTitle.AutoSize = true;
            this.loadTitle.Location = new System.Drawing.Point(335, 27);
            this.loadTitle.Name = "loadTitle";
            this.loadTitle.Size = new System.Drawing.Size(460, 32);
            this.loadTitle.TabIndex = 0;
            this.loadTitle.Text = "This is the name of Loaded Entry";
            // 
            // pnDB
            // 
            this.pnDB.AutoScroll = true;
            this.pnDB.BackColor = System.Drawing.SystemColors.Window;
            this.pnDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDB.Location = new System.Drawing.Point(3, 3);
            this.pnDB.Name = "pnDB";
            this.pnDB.Size = new System.Drawing.Size(1143, 511);
            this.pnDB.TabIndex = 0;
            // 
            // UserControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Name = "UserControlForm";
            this.Size = new System.Drawing.Size(1157, 698);
            this.tabControl1.ResumeLayout(false);
            this.tabSummary.ResumeLayout(false);
            this.tabSummary.PerformLayout();
            this.tabConf.ResumeLayout(false);
            this.tabSource.ResumeLayout(false);
            this.tabCdm.ResumeLayout(false);
            this.tabCdm.PerformLayout();
            this.tabRuntime.ResumeLayout(false);
            this.tabRuntime.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSummary;
        private System.Windows.Forms.TabPage tabConf;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label loadTitle;
        private System.Windows.Forms.TabPage tabSource;
        private System.Windows.Forms.TabPage tabRuntime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabCdm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnConf;
        private System.Windows.Forms.Panel pnDB;
    }
}
