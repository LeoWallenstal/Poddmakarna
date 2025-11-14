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
            lblMyPods = new Label();
            cbCategories = new ComboBox();
            tbRssUrl = new TextBox();
            btnGetRss = new Button();
            btnSave = new Button();
            btnDelete = new Button();
            btnDebugFetchPods = new Button();
            SuspendLayout();
            // 
            // flpMyPods
            // 
            flpMyPods.AutoScroll = true;
            flpMyPods.Location = new Point(12, 94);
            flpMyPods.Name = "flpMyPods";
            flpMyPods.Size = new Size(574, 460);
            flpMyPods.TabIndex = 0;
            // 
            // pPodPanel
            // 
            pPodPanel.Location = new Point(609, 94);
            pPodPanel.Name = "pPodPanel";
            pPodPanel.Size = new Size(860, 460);
            pPodPanel.TabIndex = 1;
            // 
            // lblMyPods
            // 
            lblMyPods.AutoSize = true;
            lblMyPods.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMyPods.Location = new Point(12, 53);
            lblMyPods.Name = "lblMyPods";
            lblMyPods.Size = new Size(174, 38);
            lblMyPods.TabIndex = 2;
            lblMyPods.Text = "Mina Poddar";
            // 
            // cbCategories
            // 
            cbCategories.FormattingEnabled = true;
            cbCategories.Items.AddRange(new object[] { "Alla Kategorier" });
            cbCategories.Location = new Point(359, 53);
            cbCategories.Name = "cbCategories";
            cbCategories.Size = new Size(227, 33);
            cbCategories.TabIndex = 3;
            // 
            // tbRssUrl
            // 
            tbRssUrl.Location = new Point(1061, 57);
            tbRssUrl.Name = "tbRssUrl";
            tbRssUrl.PlaceholderText = "Hämta rss url";
            tbRssUrl.Size = new Size(290, 31);
            tbRssUrl.TabIndex = 4;
            // 
            // btnGetRss
            // 
            btnGetRss.Location = new Point(1357, 57);
            btnGetRss.Name = "btnGetRss";
            btnGetRss.Size = new Size(112, 31);
            btnGetRss.TabIndex = 5;
            btnGetRss.Text = "Hämta";
            btnGetRss.UseVisualStyleBackColor = true;
            btnGetRss.Click += btnGetRss_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(1357, 560);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(112, 34);
            btnSave.TabIndex = 6;
            btnSave.Text = "Spara";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(1357, 560);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(112, 34);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Ta bort";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnDebugFetchPods
            // 
            btnDebugFetchPods.Location = new Point(19, 9);
            btnDebugFetchPods.Name = "btnDebugFetchPods";
            btnDebugFetchPods.Size = new Size(214, 34);
            btnDebugFetchPods.TabIndex = 8;
            btnDebugFetchPods.Text = "[Debug]Hämta Poddar";
            btnDebugFetchPods.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1518, 602);
            Controls.Add(btnDebugFetchPods);
            Controls.Add(btnDelete);
            Controls.Add(btnSave);
            Controls.Add(btnGetRss);
            Controls.Add(tbRssUrl);
            Controls.Add(cbCategories);
            Controls.Add(lblMyPods);
            Controls.Add(pPodPanel);
            Controls.Add(flpMyPods);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flpMyPods;
        private Panel pPodPanel;
        private Label lblMyPods;
        private ComboBox cbCategories;
        private TextBox tbRssUrl;
        private Button btnGetRss;
        private Button btnSave;
        private Button btnDelete;
        private Button btnDebugFetchPods;
    }
}