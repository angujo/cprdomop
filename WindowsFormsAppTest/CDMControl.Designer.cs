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
            this.label6 = new System.Windows.Forms.Label();
            this.nudTestChunk = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nudStart = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.nudEnd = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.btnClean = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChunkSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudParallels)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestChunk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.btnClean);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.btnReset);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnSchedule);
            this.groupBox2.Location = new System.Drawing.Point(3, 253);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(572, 129);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Runtime";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(111, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(455, 17);
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
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(314, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(252, 13);
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
            this.label4.Location = new System.Drawing.Point(314, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(252, 42);
            this.label4.TabIndex = 5;
            this.label4.Text = "Maximum number of parallel chunks to run. Table Deadlocks should be considered wh" +
    "en setting this number";
            // 
            // btnSaveConf
            // 
            this.btnSaveConf.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSaveConf.Location = new System.Drawing.Point(458, 214);
            this.btnSaveConf.Name = "btnSaveConf";
            this.btnSaveConf.Size = new System.Drawing.Size(108, 26);
            this.btnSaveConf.TabIndex = 6;
            this.btnSaveConf.Text = "Save Configuration";
            this.btnSaveConf.UseVisualStyleBackColor = true;
            this.btnSaveConf.Click += new System.EventHandler(this.workLoadSave);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.nudEnd);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.nudStart);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnSaveConf);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.nudTestChunk);
            this.groupBox1.Controls.Add(this.nudParallels);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nudChunkSize);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 246);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chunk Configuration";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(314, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(252, 42);
            this.label6.TabIndex = 5;
            this.label6.Text = "A number between 1-10 to count number of chunks to run. This should be used when " +
    "testing a run. Set to 0 for a full run.";
            // 
            // nudTestChunk
            // 
            this.nudTestChunk.Location = new System.Drawing.Point(92, 94);
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
            this.label7.Location = new System.Drawing.Point(6, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Chunks To Run";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Chunk Start Bound";
            // 
            // nudStart
            // 
            this.nudStart.Location = new System.Drawing.Point(133, 158);
            this.nudStart.Name = "nudStart";
            this.nudStart.Size = new System.Drawing.Size(120, 20);
            this.nudStart.TabIndex = 8;
            this.nudStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(314, 143);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(252, 34);
            this.label9.TabIndex = 2;
            this.label9.Text = "First Index to start running the Chunks from. Chunks are 0 (Zero) based.";
            // 
            // nudEnd
            // 
            this.nudEnd.Location = new System.Drawing.Point(133, 184);
            this.nudEnd.Name = "nudEnd";
            this.nudEnd.Size = new System.Drawing.Size(120, 20);
            this.nudEnd.TabIndex = 11;
            this.nudEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 186);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Chunk End Bound";
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(314, 180);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(252, 31);
            this.label11.TabIndex = 9;
            this.label11.Text = "Upper boundary to run chunks. Entry is inclusive. Set to 0(Zero) to remove bounda" +
    "ry";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(111, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(455, 17);
            this.label12.TabIndex = 5;
            this.label12.Text = "If Queue need to be reset before scheduling. Ensure the service is stopped first." +
    "";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(7, 53);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(98, 23);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset Queue";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(109, 88);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(455, 38);
            this.label13.TabIndex = 7;
            this.label13.Text = "While the service will try and perform cleanup when no job is running, you can cl" +
    "ean this to perform extensive cleanup. Ensure service is stopped first.";
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(5, 82);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(98, 23);
            this.btnClean.TabIndex = 6;
            this.btnClean.Text = "Clean Chunks";
            this.btnClean.UseVisualStyleBackColor = true;
            // 
            // CDMControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CDMControl";
            this.Size = new System.Drawing.Size(578, 385);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChunkSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudParallels)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestChunk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEnd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSchedule;
        private System.Windows.Forms.Label label3;
        private System.Diagnostics.EventLog eventLog;
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudStart;
        private System.Windows.Forms.NumericUpDown nudEnd;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnReset;
    }
}
