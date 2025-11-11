namespace Poddmakarna
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtBoxURL = new TextBox();
            btnGetPodcast = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // txtBoxURL
            // 
            txtBoxURL.Location = new Point(39, 29);
            txtBoxURL.Name = "txtBoxURL";
            txtBoxURL.Size = new Size(150, 31);
            txtBoxURL.TabIndex = 0;
            // 
            // btnGetPodcast
            // 
            btnGetPodcast.Location = new Point(212, 30);
            btnGetPodcast.Name = "btnGetPodcast";
            btnGetPodcast.Size = new Size(112, 34);
            btnGetPodcast.TabIndex = 1;
            btnGetPodcast.Text = "Hämta";
            btnGetPodcast.UseVisualStyleBackColor = true;
            btnGetPodcast.Click += btnGetPodcast_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(53, 99);
            label1.Name = "label1";
            label1.Size = new Size(59, 25);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(btnGetPodcast);
            Controls.Add(txtBoxURL);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBoxURL;
        private Button btnGetPodcast;
        private Label label1;
    }
}
