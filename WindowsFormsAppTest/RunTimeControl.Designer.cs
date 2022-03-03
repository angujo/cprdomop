namespace WindowsFormsAppTest
{
    partial class RunTimeControl
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbChunks = new System.Windows.Forms.ProgressBar();
            this.lvChunks = new System.Windows.Forms.ListView();
            this.chChunkId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chEnd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pbQuery = new System.Windows.Forms.ProgressBar();
            this.lvQuery = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chQStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chQEnd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chQStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(850, 507);
            this.splitContainer1.SplitterDistance = 336;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pbChunks);
            this.groupBox1.Controls.Add(this.lvChunks);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(330, 501);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chunks Progress";
            // 
            // pbChunks
            // 
            this.pbChunks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbChunks.Location = new System.Drawing.Point(7, 20);
            this.pbChunks.MarqueeAnimationSpeed = 10;
            this.pbChunks.Name = "pbChunks";
            this.pbChunks.Size = new System.Drawing.Size(317, 10);
            this.pbChunks.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbChunks.TabIndex = 1;
            this.pbChunks.Visible = false;
            // 
            // lvChunks
            // 
            this.lvChunks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvChunks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chChunkId,
            this.chStart,
            this.chEnd,
            this.chStatus});
            this.lvChunks.FullRowSelect = true;
            this.lvChunks.HideSelection = false;
            this.lvChunks.Location = new System.Drawing.Point(6, 36);
            this.lvChunks.MultiSelect = false;
            this.lvChunks.Name = "lvChunks";
            this.lvChunks.Size = new System.Drawing.Size(318, 459);
            this.lvChunks.TabIndex = 0;
            this.lvChunks.UseCompatibleStateImageBehavior = false;
            this.lvChunks.View = System.Windows.Forms.View.Details;
            this.lvChunks.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvChunks_ItemSelectionChanged);
            this.lvChunks.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvChunks_MouseDoubleClick);
            // 
            // chChunkId
            // 
            this.chChunkId.Text = "Ordinal";
            // 
            // chStart
            // 
            this.chStart.Text = "Start";
            this.chStart.Width = 78;
            // 
            // chEnd
            // 
            this.chEnd.Text = "Ended";
            this.chEnd.Width = 71;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            this.chStatus.Width = 101;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.pbQuery);
            this.groupBox2.Controls.Add(this.lvQuery);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(504, 501);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Query Runtime";
            // 
            // pbQuery
            // 
            this.pbQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbQuery.Location = new System.Drawing.Point(7, 20);
            this.pbQuery.MarqueeAnimationSpeed = 10;
            this.pbQuery.Name = "pbQuery";
            this.pbQuery.Size = new System.Drawing.Size(377, 10);
            this.pbQuery.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbQuery.TabIndex = 1;
            this.pbQuery.Visible = false;
            // 
            // lvQuery
            // 
            this.lvQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvQuery.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chQStart,
            this.chQEnd,
            this.chQStatus});
            this.lvQuery.FullRowSelect = true;
            this.lvQuery.HideSelection = false;
            this.lvQuery.Location = new System.Drawing.Point(6, 36);
            this.lvQuery.MultiSelect = false;
            this.lvQuery.Name = "lvQuery";
            this.lvQuery.Size = new System.Drawing.Size(492, 459);
            this.lvQuery.TabIndex = 0;
            this.lvQuery.UseCompatibleStateImageBehavior = false;
            this.lvQuery.View = System.Windows.Forms.View.Details;
            this.lvQuery.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvQuery_MouseDoubleClick);
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 112;
            // 
            // chQStart
            // 
            this.chQStart.Text = "Start";
            this.chQStart.Width = 108;
            // 
            // chQEnd
            // 
            this.chQEnd.Text = "End";
            this.chQEnd.Width = 96;
            // 
            // chQStatus
            // 
            this.chQStatus.Text = "Status";
            this.chQStatus.Width = 168;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(404, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RunTimeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.splitContainer1);
            this.Name = "RunTimeControl";
            this.Size = new System.Drawing.Size(850, 507);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvChunks;
        private System.Windows.Forms.ColumnHeader chChunkId;
        private System.Windows.Forms.ColumnHeader chStart;
        private System.Windows.Forms.ColumnHeader chEnd;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ListView lvQuery;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chQStart;
        private System.Windows.Forms.ColumnHeader chQEnd;
        private System.Windows.Forms.ColumnHeader chQStatus;
        private System.Windows.Forms.ProgressBar pbChunks;
        private System.Windows.Forms.ProgressBar pbQuery;
        private System.Windows.Forms.Button button1;
    }
}
