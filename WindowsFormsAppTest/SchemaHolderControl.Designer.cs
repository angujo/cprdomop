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
            this.dsTarget = new WindowsFormsAppTest.DBSchemaControl();
            this.dsVocabulary = new WindowsFormsAppTest.DBSchemaControl();
            this.dsSource = new WindowsFormsAppTest.DBSchemaControl();
            this.pnHolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnHolder
            // 
            this.pnHolder.Controls.Add(this.dsTarget);
            this.pnHolder.Controls.Add(this.dsVocabulary);
            this.pnHolder.Controls.Add(this.dsSource);
            this.pnHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnHolder.Location = new System.Drawing.Point(0, 0);
            this.pnHolder.Name = "pnHolder";
            this.pnHolder.Size = new System.Drawing.Size(654, 1048);
            this.pnHolder.TabIndex = 0;
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
            this.Size = new System.Drawing.Size(654, 1048);
            this.pnHolder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnHolder;
        private DBSchemaControl dsSource;
        private DBSchemaControl dsVocabulary;
        private DBSchemaControl dsTarget;
    }
}
