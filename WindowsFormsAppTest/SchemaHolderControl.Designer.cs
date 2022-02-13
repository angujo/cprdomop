namespace WindowsFormsAppTest
{
    partial class SchemaHolderControl
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
            this.pnHolder = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sourceProcessControl = new WindowsFormsAppTest.SourceProcessControl();
            this.dsTarget = new WindowsFormsAppTest.DBSchemaControl();
            this.dsVocabulary = new WindowsFormsAppTest.DBSchemaControl();
            this.dsSource = new WindowsFormsAppTest.DBSchemaControl();
            this.pnHolder.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnHolder
            // 
            this.pnHolder.Controls.Add(this.panel1);
            this.pnHolder.Controls.Add(this.dsTarget);
            this.pnHolder.Controls.Add(this.dsVocabulary);
            this.pnHolder.Controls.Add(this.dsSource);
            this.pnHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnHolder.Location = new System.Drawing.Point(0, 0);
            this.pnHolder.Name = "pnHolder";
            this.pnHolder.Size = new System.Drawing.Size(1342, 1048);
            this.pnHolder.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.sourceProcessControl);
            this.panel1.Location = new System.Drawing.Point(666, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(673, 1029);
            this.panel1.TabIndex = 3;
            // 
            // sourceProcessControl
            // 
            this.sourceProcessControl.AutoScroll = true;
            this.sourceProcessControl.AutoSize = true;
            this.sourceProcessControl.Location = new System.Drawing.Point(3, 3);
            this.sourceProcessControl.Name = "sourceProcessControl";
            this.sourceProcessControl.Size = new System.Drawing.Size(699, 1074);
            this.sourceProcessControl.TabIndex = 0;
            // 
            // dsTarget
            // 
            this.dsTarget.Location = new System.Drawing.Point(3, 694);
            this.dsTarget.Name = "dsTarget";
            this.dsTarget.Size = new System.Drawing.Size(625, 338);
            this.dsTarget.TabIndex = 2;
            // 
            // dsVocabulary
            // 
            this.dsVocabulary.Location = new System.Drawing.Point(3, 347);
            this.dsVocabulary.Name = "dsVocabulary";
            this.dsVocabulary.Size = new System.Drawing.Size(625, 338);
            this.dsVocabulary.TabIndex = 1;
            // 
            // dsSource
            // 
            this.dsSource.Location = new System.Drawing.Point(3, 3);
            this.dsSource.Name = "dsSource";
            this.dsSource.Size = new System.Drawing.Size(625, 338);
            this.dsSource.TabIndex = 0;
            // 
            // SchemaHolderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnHolder);
            this.Name = "SchemaHolderControl";
            this.Size = new System.Drawing.Size(1342, 1048);
            this.pnHolder.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnHolder;
        private DBSchemaControl dsSource;
        private DBSchemaControl dsVocabulary;
        private DBSchemaControl dsTarget;
        private System.Windows.Forms.Panel panel1;
        private SourceProcessControl sourceProcessControl;
    }
}
