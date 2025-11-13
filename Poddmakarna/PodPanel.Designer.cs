namespace UI
{
    partial class PodPanel
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
            rtbPodDesc = new RichTextBox();
            rtbPodTitle = new RichTextBox();
            dgvEpisodes = new DataGridView();
            colTitle = new DataGridViewTextBoxColumn();
            lblEpTitle = new Label();
            lblEpDate = new Label();
            rtbEpDesc = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)pbThumbnail).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvEpisodes).BeginInit();
            SuspendLayout();
            // 
            // pbThumbnail
            // 
            pbThumbnail.Location = new Point(3, 3);
            pbThumbnail.Name = "pbThumbnail";
            pbThumbnail.Size = new Size(150, 150);
            pbThumbnail.TabIndex = 0;
            pbThumbnail.TabStop = false;
            // 
            // rtbPodDesc
            // 
            rtbPodDesc.BackColor = SystemColors.Menu;
            rtbPodDesc.BorderStyle = BorderStyle.None;
            rtbPodDesc.Location = new Point(159, 54);
            rtbPodDesc.Name = "rtbPodDesc";
            rtbPodDesc.ReadOnly = true;
            rtbPodDesc.Size = new Size(303, 99);
            rtbPodDesc.TabIndex = 3;
            rtbPodDesc.TabStop = false;
            rtbPodDesc.Text = "Beskrivning";
            // 
            // rtbPodTitle
            // 
            rtbPodTitle.BackColor = SystemColors.Menu;
            rtbPodTitle.BorderStyle = BorderStyle.None;
            rtbPodTitle.Cursor = Cursors.Hand;
            rtbPodTitle.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbPodTitle.Location = new Point(159, 3);
            rtbPodTitle.Name = "rtbPodTitle";
            rtbPodTitle.Size = new Size(698, 45);
            rtbPodTitle.TabIndex = 4;
            rtbPodTitle.Text = "Titel";
            // 
            // dgvEpisodes
            // 
            dgvEpisodes.AllowUserToAddRows = false;
            dgvEpisodes.AllowUserToDeleteRows = false;
            dgvEpisodes.AllowUserToResizeColumns = false;
            dgvEpisodes.AllowUserToResizeRows = false;
            dgvEpisodes.BackgroundColor = SystemColors.Menu;
            dgvEpisodes.BorderStyle = BorderStyle.None;
            dgvEpisodes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEpisodes.Columns.AddRange(new DataGridViewColumn[] { colTitle });
            dgvEpisodes.Cursor = Cursors.Hand;
            dgvEpisodes.Location = new Point(3, 159);
            dgvEpisodes.MultiSelect = false;
            dgvEpisodes.Name = "dgvEpisodes";
            dgvEpisodes.ReadOnly = true;
            dgvEpisodes.RowHeadersVisible = false;
            dgvEpisodes.RowHeadersWidth = 62;
            dgvEpisodes.Size = new Size(459, 298);
            dgvEpisodes.TabIndex = 5;
            // 
            // colTitle
            // 
            colTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTitle.DataPropertyName = "Title";
            colTitle.HeaderText = "Avsnitt";
            colTitle.MinimumWidth = 8;
            colTitle.Name = "colTitle";
            colTitle.ReadOnly = true;
            colTitle.Resizable = DataGridViewTriState.False;
            colTitle.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // lblEpTitle
            // 
            lblEpTitle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEpTitle.Location = new Point(468, 159);
            lblEpTitle.Name = "lblEpTitle";
            lblEpTitle.Size = new Size(409, 32);
            lblEpTitle.TabIndex = 6;
            lblEpTitle.Text = "Titel på avsnitt";
            // 
            // lblEpDate
            // 
            lblEpDate.AutoSize = true;
            lblEpDate.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblEpDate.Location = new Point(468, 196);
            lblEpDate.Name = "lblEpDate";
            lblEpDate.Size = new Size(106, 25);
            lblEpDate.TabIndex = 7;
            lblEpDate.Text = "2025-11-12";
            // 
            // rtbEpDesc
            // 
            rtbEpDesc.BackColor = SystemColors.Menu;
            rtbEpDesc.BorderStyle = BorderStyle.None;
            rtbEpDesc.Location = new Point(468, 224);
            rtbEpDesc.Name = "rtbEpDesc";
            rtbEpDesc.ReadOnly = true;
            rtbEpDesc.Size = new Size(384, 228);
            rtbEpDesc.TabIndex = 9;
            rtbEpDesc.TabStop = false;
            rtbEpDesc.Text = "";
            // 
            // PodPanel
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(rtbEpDesc);
            Controls.Add(lblEpDate);
            Controls.Add(lblEpTitle);
            Controls.Add(dgvEpisodes);
            Controls.Add(rtbPodTitle);
            Controls.Add(rtbPodDesc);
            Controls.Add(pbThumbnail);
            Name = "PodPanel";
            Padding = new Padding(5);
            Size = new Size(860, 460);
            ((System.ComponentModel.ISupportInitialize)pbThumbnail).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvEpisodes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbThumbnail;
        private RichTextBox rtbPodDesc;
        private RichTextBox rtbPodTitle;
        private DataGridView dgvEpisodes;
        private Label lblEpTitle;
        private Label lblEpDate;
        private DataGridViewTextBoxColumn colTitle;
        private RichTextBox rtbEpDesc;
    }
}
