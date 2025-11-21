namespace UI
{
    partial class PodCard
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
            pbThumbnail = new PictureBox();
            lblTitle = new Label();
            lblCategory = new Label();
            ((System.ComponentModel.ISupportInitialize)pbThumbnail).BeginInit();
            SuspendLayout();
            // 
            // pbThumbnail
            // 
            pbThumbnail.Location = new Point(10, 10);
            pbThumbnail.Name = "pbThumbnail";
            pbThumbnail.Size = new Size(96, 96);
            pbThumbnail.TabIndex = 0;
            pbThumbnail.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(112, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(73, 36);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Titel";
            lblTitle.UseMnemonic = false;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(112, 55);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(0, 25);
            lblCategory.TabIndex = 2;
            // 
            // PodCard
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblCategory);
            Controls.Add(lblTitle);
            Controls.Add(pbThumbnail);
            Name = "PodCard";
            Size = new Size(531, 116);
            ((System.ComponentModel.ISupportInitialize)pbThumbnail).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbThumbnail;
        private Label lblTitle;
        private Label lblCategory;
    }
}
