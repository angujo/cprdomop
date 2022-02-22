namespace WindowsFormsAppTest
{
    partial class CDMControl
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.eventLog = new System.Diagnostics.EventLog();
            this.label1 = new System.Windows.Forms.Label();
            this.nudChunkSize = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudParallels = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSaveConf = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSaveTest = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.nudTestChunk = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChunkSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudParallels)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestChunk)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnSchedule);
            this.groupBox2.Location = new System.Drawing.Point(3, 194);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(485, 60);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Runtime";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(111, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Initiate a run and push to schedule for background run";
            // 
            // btnSchedule
            // 
            this.btnSchedule.Location = new System.Drawing.Point(7, 20);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.Size = new System.Drawing.Size(98, 23);
            this.btnSchedule.TabIndex = 0;
            this.btnSchedule.Text = "Schedule Run";
            this.btnSchedule.UseVisualStyleBackColor = true;
            this.btnSchedule.Click += new System.EventHandler(this.btnSchedule_Click);
            // 
            // eventLog
            // 
            this.eventLog.SynchronizingObject = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chunk Size";
            // 
            // nudChunkSize
            // 
            this.nudChunkSize.Location = new System.Drawing.Point(92, 19);
            this.nudChunkSize.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nudChunkSize.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudChunkSize.Name = "nudChunkSize";
            this.nudChunkSize.Size = new System.Drawing.Size(130, 20);
            this.nudChunkSize.TabIndex = 1;
            this.nudChunkSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudChunkSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(240, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(216, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "The number of records to run on each chunk.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Max Parallellism";
            // 
            // nudParallels
            // 
            this.nudParallels.Location = new System.Drawing.Point(92, 45);
            this.nudParallels.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudParallels.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudParallels.Name = "nudParallels";
            this.nudParallels.Size = new System.Drawing.Size(130, 20);
            this.nudParallels.TabIndex = 4;
            this.nudParallels.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudParallels.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(240, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(239, 42);
            this.label4.TabIndex = 5;
            this.label4.Text = "Maximum number of parallel chunks to run. Table Deadlocks should be considered wh" +
    "en setting this number";
            // 
            // btnSaveConf
            // 
            this.btnSaveConf.Location = new System.Drawing.Point(114, 75);
            this.btnSaveConf.Name = "btnSaveConf";
            this.btnSaveConf.Size = new System.Drawing.Size(108, 23);
            this.btnSaveConf.TabIndex = 6;
            this.btnSaveConf.Text = "Save Configuration";
            this.btnSaveConf.UseVisualStyleBackColor = true;
            this.btnSaveConf.Click += new System.EventHandler(this.workLoadSave);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnSaveConf);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.nudParallels);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nudChunkSize);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(485, 105);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chunk Configuration";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnSaveTest);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.nudTestChunk);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(3, 114);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(485, 74);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Test Chunk Run";
            // 
            // btnSaveTest
            // 
            this.btnSaveTest.Location = new System.Drawing.Point(114, 42);
            this.btnSaveTest.Name = "btnSaveTest";
            this.btnSaveTest.Size = new System.Drawing.Size(108, 23);
            this.btnSaveTest.TabIndex = 6;
            this.btnSaveTest.Text = "Save Test";
            this.btnSaveTest.UseVisualStyleBackColor = true;
            this.btnSaveTest.Click += new System.EventHandler(this.workLoadSave);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(240, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(239, 42);
            this.label6.TabIndex = 5;
            this.label6.Text = "A number between 1-10 to count number of chunks to run. This should be used when " +
    "testing a run. Set to 0 for a full run.";
            // 
            // nudTestChunk
            // 
            this.nudTestChunk.Location = new System.Drawing.Point(92, 16);
            this.nudTestChunk.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudTestChunk.Name = "nudTestChunk";
            this.nudTestChunk.Size = new System.Drawing.Size(130, 20);
            this.nudTestChunk.TabIndex = 4;
            this.nudTestChunk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTestChunk.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Chunks To Run";
            // 
            // CDMControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CDMControl";
            this.Size = new System.Drawing.Size(491, 262);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChunkSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudParallels)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestChunk)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSchedule;
        private System.Windows.Forms.Label label3;
        private System.Diagnostics.EventLog eventLog;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSaveTest;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudTestChunk;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSaveConf;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudParallels;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudChunkSize;
        private System.Windows.Forms.Label label1;
    }
}
