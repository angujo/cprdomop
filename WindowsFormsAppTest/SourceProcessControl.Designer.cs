namespace WindowsFormsAppTest
{
    partial class SourceProcessControl
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
            this.lvQueue = new System.Windows.Forms.ListView();
            this.clhName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhFilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmhProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnQueue = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvQueue
            // 
            this.lvQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvQueue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhName,
            this.clhFilePath,
            this.clmhProgress});
            this.lvQueue.FullRowSelect = true;
            this.lvQueue.GridLines = true;
            this.lvQueue.HideSelection = false;
            this.lvQueue.Location = new System.Drawing.Point(23, 214);
            this.lvQueue.Name = "lvQueue";
            this.lvQueue.Size = new System.Drawing.Size(533, 761);
            this.lvQueue.TabIndex = 0;
            this.lvQueue.UseCompatibleStateImageBehavior = false;
            this.lvQueue.View = System.Windows.Forms.View.Details;
            // 
            // clhName
            // 
            this.clhName.Text = "Name";
            this.clhName.Width = 112;
            // 
            // clhFilePath
            // 
            this.clhFilePath.Text = "File Path";
            this.clhFilePath.Width = 248;
            // 
            // clmhProgress
            // 
            this.clmhProgress.Text = "Progress";
            this.clmhProgress.Width = 188;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.lvQueue);
            this.groupBox1.Location = new System.Drawing.Point(26, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(592, 1011);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source File Processing";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.btnQueue);
            this.groupBox3.Location = new System.Drawing.Point(7, 106);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(572, 74);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Queue Load";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(149, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(417, 55);
            this.label2.TabIndex = 1;
            this.label2.Text = "Send the queue to the runtime for the next job schedule for execution.";
            // 
            // btnQueue
            // 
            this.btnQueue.Location = new System.Drawing.Point(7, 26);
            this.btnQueue.Name = "btnQueue";
            this.btnQueue.Size = new System.Drawing.Size(132, 42);
            this.btnQueue.TabIndex = 0;
            this.btnQueue.Text = "Load Queue";
            this.btnQueue.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnLoad);
            this.groupBox2.Location = new System.Drawing.Point(7, 26);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(572, 74);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Process Load";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(145, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(421, 54);
            this.label1.TabIndex = 1;
            this.label1.Text = "Check and load the source file queue. This will generate the queue to be used for" +
    " processing the source files.";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(7, 26);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(132, 42);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load Process";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // SourceProcessControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.groupBox1);
            this.Name = "SourceProcessControl";
            this.Size = new System.Drawing.Size(650, 1080);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvQueue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColumnHeader clhName;
        private System.Windows.Forms.ColumnHeader clhFilePath;
        private System.Windows.Forms.ColumnHeader clmhProgress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnQueue;
    }
}
