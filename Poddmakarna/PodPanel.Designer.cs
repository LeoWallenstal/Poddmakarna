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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            pbThumbnail = new PictureBox();
            rtbPodDesc = new RichTextBox();
            rtbPodTitle = new RichTextBox();
            dgvEpisodes = new DataGridView();
            colTitle = new DataGridViewTextBoxColumn();
            lblEpTitle = new Label();
            lblEpDate = new Label();
            rtbEpDesc = new RichTextBox();
            cbCategory = new ComboBox();
            lblCategory = new Label();
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
            rtbPodTitle.Multiline = false;
            rtbPodTitle.Name = "rtbPodTitle";
            rtbPodTitle.Size = new Size(420, 45);
            rtbPodTitle.TabIndex = 4;
            rtbPodTitle.Text = "Titel";
            rtbPodTitle.Leave += rtbPodTitle_Leave;
            // 
            // dgvEpisodes
            // 
            dgvEpisodes.AllowUserToAddRows = false;
            dgvEpisodes.AllowUserToDeleteRows = false;
            dgvEpisodes.AllowUserToResizeColumns = false;
            dgvEpisodes.AllowUserToResizeRows = false;
            dgvEpisodes.BackgroundColor = SystemColors.Menu;
            dgvEpisodes.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvEpisodes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvEpisodes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEpisodes.Columns.AddRange(new DataGridViewColumn[] { colTitle });
            dgvEpisodes.Cursor = Cursors.Hand;
            dgvEpisodes.Location = new Point(3, 196);
            dgvEpisodes.MultiSelect = false;
            dgvEpisodes.Name = "dgvEpisodes";
            dgvEpisodes.ReadOnly = true;
            dgvEpisodes.RowHeadersVisible = false;
            dgvEpisodes.RowHeadersWidth = 62;
            dgvEpisodes.Size = new Size(459, 256);
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
            lblEpTitle.Location = new Point(468, 196);
            lblEpTitle.Name = "lblEpTitle";
            lblEpTitle.Size = new Size(409, 32);
            lblEpTitle.TabIndex = 6;
            lblEpTitle.Text = "Titel på avsnitt";
            lblEpTitle.UseMnemonic = false;
            // 
            // lblEpDate
            // 
            lblEpDate.AutoSize = true;
            lblEpDate.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblEpDate.Location = new Point(468, 233);
            lblEpDate.Name = "lblEpDate";
            lblEpDate.Size = new Size(106, 25);
            lblEpDate.TabIndex = 7;
            lblEpDate.Text = "2025-11-12";
            // 
            // rtbEpDesc
            // 
            rtbEpDesc.BackColor = SystemColors.Menu;
            rtbEpDesc.BorderStyle = BorderStyle.None;
            rtbEpDesc.Location = new Point(468, 261);
            rtbEpDesc.Name = "rtbEpDesc";
            rtbEpDesc.ReadOnly = true;
            rtbEpDesc.Size = new Size(392, 191);
            rtbEpDesc.TabIndex = 9;
            rtbEpDesc.TabStop = false;
            rtbEpDesc.Text = "";
            // 
            // cbCategory
            // 
            cbCategory.FormattingEnabled = true;
            cbCategory.Location = new Point(87, 159);
            cbCategory.Name = "cbCategory";
            cbCategory.Size = new Size(207, 33);
            cbCategory.TabIndex = 10;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(3, 165);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(78, 25);
            lblCategory.TabIndex = 11;
            lblCategory.Text = "Kategori";
            // 
            // PodPanel
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblCategory);
            Controls.Add(cbCategory);
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
        private ComboBox cbCategory;
        private Label lblCategory;
    }
}
