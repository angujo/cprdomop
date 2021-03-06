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
            this.tabsHolder = new System.Windows.Forms.TabControl();
            this.tabSummary = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.tabConf = new System.Windows.Forms.TabPage();
            this.pnConf = new System.Windows.Forms.Panel();
            this.tabSchemas = new System.Windows.Forms.TabPage();
            this.pnDB = new System.Windows.Forms.Panel();
            this.tabSource = new System.Windows.Forms.TabPage();
            this.pnSource = new System.Windows.Forms.Panel();
            this.tabCdm = new System.Windows.Forms.TabPage();
            this.pnCdm = new System.Windows.Forms.Panel();
            this.tabRuntime = new System.Windows.Forms.TabPage();
            this.pnRuntime = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.loadTitle = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.tabsHolder.SuspendLayout();
            this.tabSummary.SuspendLayout();
            this.tabConf.SuspendLayout();
            this.tabSchemas.SuspendLayout();
            this.tabSource.SuspendLayout();
            this.tabCdm.SuspendLayout();
            this.tabRuntime.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabsHolder
            // 
            this.tabsHolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabsHolder.Controls.Add(this.tabSummary);
            this.tabsHolder.Controls.Add(this.tabConf);
            this.tabsHolder.Controls.Add(this.tabSchemas);
            this.tabsHolder.Controls.Add(this.tabSource);
            this.tabsHolder.Controls.Add(this.tabCdm);
            this.tabsHolder.Controls.Add(this.tabRuntime);
            this.tabsHolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabsHolder.Location = new System.Drawing.Point(0, 121);
            this.tabsHolder.Margin = new System.Windows.Forms.Padding(2);
            this.tabsHolder.Name = "tabsHolder";
            this.tabsHolder.Padding = new System.Drawing.Point(10, 5);
            this.tabsHolder.SelectedIndex = 0;
            this.tabsHolder.Size = new System.Drawing.Size(771, 330);
            this.tabsHolder.TabIndex = 1;
            this.tabsHolder.SelectedIndexChanged += new System.EventHandler(this.tabsHolder_SelectedIndexChanged);
            this.tabsHolder.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabsHolder_Selected);
            // 
            // tabSummary
            // 
            this.tabSummary.BackColor = System.Drawing.SystemColors.Window;
            this.tabSummary.Controls.Add(this.label2);
            this.tabSummary.Location = new System.Drawing.Point(4, 29);
            this.tabSummary.Margin = new System.Windows.Forms.Padding(2);
            this.tabSummary.Name = "tabSummary";
            this.tabSummary.Padding = new System.Windows.Forms.Padding(2);
            this.tabSummary.Size = new System.Drawing.Size(763, 297);
            this.tabSummary.TabIndex = 0;
            this.tabSummary.Text = "Summary";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(318, 185);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "For Tab 101";
            // 
            // tabConf
            // 
            this.tabConf.Controls.Add(this.pnConf);
            this.tabConf.Location = new System.Drawing.Point(4, 29);
            this.tabConf.Margin = new System.Windows.Forms.Padding(2);
            this.tabConf.Name = "tabConf";
            this.tabConf.Padding = new System.Windows.Forms.Padding(2);
            this.tabConf.Size = new System.Drawing.Size(763, 297);
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
            this.pnConf.Location = new System.Drawing.Point(18, 19);
            this.pnConf.Margin = new System.Windows.Forms.Padding(2);
            this.pnConf.Name = "pnConf";
            this.pnConf.Size = new System.Drawing.Size(723, 301);
            this.pnConf.TabIndex = 0;
            // 
            // tabSchemas
            // 
            this.tabSchemas.BackColor = System.Drawing.SystemColors.Window;
            this.tabSchemas.Controls.Add(this.pnDB);
            this.tabSchemas.Location = new System.Drawing.Point(4, 29);
            this.tabSchemas.Margin = new System.Windows.Forms.Padding(2);
            this.tabSchemas.Name = "tabSchemas";
            this.tabSchemas.Padding = new System.Windows.Forms.Padding(2);
            this.tabSchemas.Size = new System.Drawing.Size(763, 297);
            this.tabSchemas.TabIndex = 2;
            this.tabSchemas.Text = "Schema Connections";
            // 
            // pnDB
            // 
            this.pnDB.AutoScroll = true;
            this.pnDB.BackColor = System.Drawing.SystemColors.Window;
            this.pnDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDB.Location = new System.Drawing.Point(2, 2);
            this.pnDB.Margin = new System.Windows.Forms.Padding(2);
            this.pnDB.Name = "pnDB";
            this.pnDB.Size = new System.Drawing.Size(759, 293);
            this.pnDB.TabIndex = 0;
            // 
            // tabSource
            // 
            this.tabSource.BackColor = System.Drawing.SystemColors.Window;
            this.tabSource.Controls.Add(this.pnSource);
            this.tabSource.Location = new System.Drawing.Point(4, 29);
            this.tabSource.Margin = new System.Windows.Forms.Padding(2);
            this.tabSource.Name = "tabSource";
            this.tabSource.Padding = new System.Windows.Forms.Padding(2);
            this.tabSource.Size = new System.Drawing.Size(763, 297);
            this.tabSource.TabIndex = 5;
            this.tabSource.Text = "Source";
            // 
            // pnSource
            // 
            this.pnSource.AutoScroll = true;
            this.pnSource.BackColor = System.Drawing.SystemColors.Window;
            this.pnSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnSource.Location = new System.Drawing.Point(2, 2);
            this.pnSource.Margin = new System.Windows.Forms.Padding(2);
            this.pnSource.Name = "pnSource";
            this.pnSource.Size = new System.Drawing.Size(759, 293);
            this.pnSource.TabIndex = 0;
            // 
            // tabCdm
            // 
            this.tabCdm.BackColor = System.Drawing.SystemColors.Window;
            this.tabCdm.Controls.Add(this.pnCdm);
            this.tabCdm.Location = new System.Drawing.Point(4, 29);
            this.tabCdm.Margin = new System.Windows.Forms.Padding(2);
            this.tabCdm.Name = "tabCdm";
            this.tabCdm.Padding = new System.Windows.Forms.Padding(2);
            this.tabCdm.Size = new System.Drawing.Size(763, 297);
            this.tabCdm.TabIndex = 3;
            this.tabCdm.Text = "OMOP CDM";
            // 
            // pnCdm
            // 
            this.pnCdm.AutoScroll = true;
            this.pnCdm.BackColor = System.Drawing.SystemColors.Window;
            this.pnCdm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnCdm.Location = new System.Drawing.Point(2, 2);
            this.pnCdm.Margin = new System.Windows.Forms.Padding(2);
            this.pnCdm.Name = "pnCdm";
            this.pnCdm.Size = new System.Drawing.Size(759, 293);
            this.pnCdm.TabIndex = 1;
            // 
            // tabRuntime
            // 
            this.tabRuntime.BackColor = System.Drawing.SystemColors.Window;
            this.tabRuntime.Controls.Add(this.pnRuntime);
            this.tabRuntime.Location = new System.Drawing.Point(4, 29);
            this.tabRuntime.Margin = new System.Windows.Forms.Padding(2);
            this.tabRuntime.Name = "tabRuntime";
            this.tabRuntime.Padding = new System.Windows.Forms.Padding(2);
            this.tabRuntime.Size = new System.Drawing.Size(763, 297);
            this.tabRuntime.TabIndex = 4;
            this.tabRuntime.Text = "Runtime";
            // 
            // pnRuntime
            // 
            this.pnRuntime.AutoScroll = true;
            this.pnRuntime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnRuntime.Location = new System.Drawing.Point(2, 2);
            this.pnRuntime.Name = "pnRuntime";
            this.pnRuntime.Size = new System.Drawing.Size(759, 293);
            this.pnRuntime.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.loadTitle);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(3, 26);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(769, 91);
            this.panel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.lblTime);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(531, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 85);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Service";
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(103, 51);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(124, 16);
            this.lblTime.TabIndex = 4;
            this.lblTime.Text = "......";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Last Run time";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Status";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(106, 13);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(125, 14);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "[Undetected]";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // loadTitle
            // 
            this.loadTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.loadTitle.Location = new System.Drawing.Point(7, 3);
            this.loadTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.loadTitle.Name = "loadTitle";
            this.loadTitle.Size = new System.Drawing.Size(519, 85);
            this.loadTitle.TabIndex = 0;
            this.loadTitle.Text = "This is the name of Loaded Entry";
            this.loadTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Name";
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(108, 34);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(125, 14);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "[UNKNOWN]";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // UserControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabsHolder);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UserControlForm";
            this.Size = new System.Drawing.Size(771, 454);
            this.tabsHolder.ResumeLayout(false);
            this.tabSummary.ResumeLayout(false);
            this.tabSummary.PerformLayout();
            this.tabConf.ResumeLayout(false);
            this.tabSchemas.ResumeLayout(false);
            this.tabSource.ResumeLayout(false);
            this.tabCdm.ResumeLayout(false);
            this.tabRuntime.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabsHolder;
        private System.Windows.Forms.TabPage tabSummary;
        private System.Windows.Forms.TabPage tabConf;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label loadTitle;
        private System.Windows.Forms.TabPage tabSchemas;
        private System.Windows.Forms.TabPage tabRuntime;
        private System.Windows.Forms.TabPage tabCdm;
        private System.Windows.Forms.Panel pnConf;
        private System.Windows.Forms.Panel pnDB;
        private System.Windows.Forms.TabPage tabSource;
        private System.Windows.Forms.Panel pnSource;
        private System.Windows.Forms.Panel pnCdm;
        private System.Windows.Forms.Panel pnRuntime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblName;
    }
}
