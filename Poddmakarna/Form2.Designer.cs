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
            pCategoryPanel = new Panel();
            btnDebugRemovePodcasts = new Button();
            cbUpdateFreq = new ComboBox();
            lblUpdateFreq = new Label();
            SuspendLayout();
            // 
            // flpMyPods
            // 
            flpMyPods.AutoScroll = true;
            flpMyPods.Location = new Point(12, 94);
            flpMyPods.Name = "flpMyPods";
            flpMyPods.Size = new Size(574, 833);
            flpMyPods.TabIndex = 0;
            // 
            // pPodPanel
            // 
            pPodPanel.Location = new Point(609, 127);
            pPodPanel.Name = "pPodPanel";
            pPodPanel.Size = new Size(1272, 460);
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
            tbRssUrl.Location = new Point(1473, 53);
            tbRssUrl.Name = "tbRssUrl";
            tbRssUrl.PlaceholderText = "Hämta rss url";
            tbRssUrl.Size = new Size(290, 31);
            tbRssUrl.TabIndex = 4;
            // 
            // btnGetRss
            // 
            btnGetRss.Location = new Point(1769, 53);
            btnGetRss.Name = "btnGetRss";
            btnGetRss.Size = new Size(112, 31);
            btnGetRss.TabIndex = 5;
            btnGetRss.Text = "Hämta";
            btnGetRss.UseVisualStyleBackColor = true;
            btnGetRss.Click += btnGetRss_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(609, 87);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(112, 34);
            btnSave.TabIndex = 6;
            btnSave.Text = "Spara";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(609, 87);
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
            // pCategoryPanel
            // 
            pCategoryPanel.Location = new Point(609, 593);
            pCategoryPanel.Name = "pCategoryPanel";
            pCategoryPanel.Size = new Size(450, 334);
            pCategoryPanel.TabIndex = 9;
            // 
            // btnDebugRemovePodcasts
            // 
            btnDebugRemovePodcasts.Location = new Point(239, 9);
            btnDebugRemovePodcasts.Name = "btnDebugRemovePodcasts";
            btnDebugRemovePodcasts.Size = new Size(243, 34);
            btnDebugRemovePodcasts.TabIndex = 10;
            btnDebugRemovePodcasts.Text = "[Debug] Ta bort alla poddar";
            btnDebugRemovePodcasts.UseVisualStyleBackColor = true;
            // 
            // cbUpdateFreq
            // 
            cbUpdateFreq.FormattingEnabled = true;
            cbUpdateFreq.Location = new Point(1704, 894);
            cbUpdateFreq.Name = "cbUpdateFreq";
            cbUpdateFreq.Size = new Size(182, 33);
            cbUpdateFreq.TabIndex = 11;
            // 
            // lblUpdateFreq
            // 
            lblUpdateFreq.AutoSize = true;
            lblUpdateFreq.Location = new Point(1513, 897);
            lblUpdateFreq.Name = "lblUpdateFreq";
            lblUpdateFreq.Size = new Size(185, 25);
            lblUpdateFreq.TabIndex = 12;
            lblUpdateFreq.Text = "Uppdateringsintervall:";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1898, 939);
            Controls.Add(lblUpdateFreq);
            Controls.Add(cbUpdateFreq);
            Controls.Add(btnDebugRemovePodcasts);
            Controls.Add(pCategoryPanel);
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
            Text = "Poddmakarna";
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
        private Panel pCategoryPanel;
        private Button btnDebugRemovePodcasts;
        private ComboBox cbUpdateFreq;
        private Label lblUpdateFreq;
    }
}