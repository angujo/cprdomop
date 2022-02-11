namespace WindowsFormsAppTest
{
    partial class WorkLoadForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.tbName = new System.Windows.Forms.TextBox();
            this.btnS = new System.Windows.Forms.Button();
            this.btnSL = new System.Windows.Forms.Button();
            this.cbSourceProcessed = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(319, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Enter or modify details about the Work Load";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Release Date";
            // 
            // dtDate
            // 
            this.dtDate.Location = new System.Drawing.Point(203, 171);
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(325, 26);
            this.dtDate.TabIndex = 3;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(203, 126);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(325, 26);
            this.tbName.TabIndex = 4;
            // 
            // btnS
            // 
            this.btnS.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnS.Location = new System.Drawing.Point(272, 262);
            this.btnS.Name = "btnS";
            this.btnS.Size = new System.Drawing.Size(91, 36);
            this.btnS.TabIndex = 5;
            this.btnS.Text = "Save";
            this.btnS.UseVisualStyleBackColor = true;
            this.btnS.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSL
            // 
            this.btnSL.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnSL.Location = new System.Drawing.Point(389, 262);
            this.btnSL.Name = "btnSL";
            this.btnSL.Size = new System.Drawing.Size(139, 36);
            this.btnSL.TabIndex = 6;
            this.btnSL.Text = "Save and Load";
            this.btnSL.UseVisualStyleBackColor = true;
            this.btnSL.Click += new System.EventHandler(this.btnSL_Click);
            // 
            // cbSourceProcessed
            // 
            this.cbSourceProcessed.AutoSize = true;
            this.cbSourceProcessed.Location = new System.Drawing.Point(203, 215);
            this.cbSourceProcessed.Name = "cbSourceProcessed";
            this.cbSourceProcessed.Size = new System.Drawing.Size(264, 24);
            this.cbSourceProcessed.TabIndex = 7;
            this.cbSourceProcessed.Text = "Source Files Loaded & Processed";
            this.cbSourceProcessed.UseVisualStyleBackColor = true;
            // 
            // WorkLoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 329);
            this.Controls.Add(this.cbSourceProcessed);
            this.Controls.Add(this.btnSL);
            this.Controls.Add(this.btnS);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.dtDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WorkLoadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WorkLoadForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Button btnS;
        private System.Windows.Forms.Button btnSL;
        private System.Windows.Forms.CheckBox cbSourceProcessed;
    }
}