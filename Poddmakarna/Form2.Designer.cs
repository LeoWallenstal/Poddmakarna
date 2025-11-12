namespace UI
{
    partial class Form2
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
            flpMyPods = new FlowLayoutPanel();
            pPodPanel = new Panel();
            SuspendLayout();
            // 
            // flpMyPods
            // 
            flpMyPods.AutoScroll = true;
            flpMyPods.Location = new Point(12, 12);
            flpMyPods.Name = "flpMyPods";
            flpMyPods.Size = new Size(574, 434);
            flpMyPods.TabIndex = 0;
            // 
            // pPodPanel
            // 
            pPodPanel.Location = new Point(609, 12);
            pPodPanel.Name = "pPodPanel";
            pPodPanel.Size = new Size(860, 460);
            pPodPanel.TabIndex = 1;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1518, 480);
            Controls.Add(pPodPanel);
            Controls.Add(flpMyPods);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flpMyPods;
        private Panel pPodPanel;
    }
}