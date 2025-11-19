namespace UI
{
    partial class CategoryPanel
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dgvCategories = new DataGridView();
            colCategory = new DataGridViewTextBoxColumn();
            btnRemove = new Button();
            btnAdd = new Button();
            tbCategory = new TextBox();
            lblError = new Label();
            btnEdit = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            SuspendLayout();
            // 
            // dgvCategories
            // 
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dgvCategories.AllowUserToResizeColumns = false;
            dgvCategories.AllowUserToResizeRows = false;
            dgvCategories.BackgroundColor = SystemColors.Menu;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvCategories.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategories.Columns.AddRange(new DataGridViewColumn[] { colCategory });
            dgvCategories.GridColor = SystemColors.Menu;
            dgvCategories.Location = new Point(3, 3);
            dgvCategories.MultiSelect = false;
            dgvCategories.Name = "dgvCategories";
            dgvCategories.RowHeadersVisible = false;
            dgvCategories.RowHeadersWidth = 62;
            dgvCategories.Size = new Size(444, 225);
            dgvCategories.TabIndex = 0;
            dgvCategories.CellBeginEdit += dgvCategories_CellBeginEdit;
            dgvCategories.CellEndEdit += dgvCategories_CellEndEdit;
            dgvCategories.CellMouseClick += dgvCategories_CellMouseClick;
            dgvCategories.DataBindingComplete += dgvCategories_DataBindingComplete;
            dgvCategories.Leave += dgvCategories_Leave;
            dgvCategories.MouseClick += dgvCategories_MouseClick;
            // 
            // colCategory
            // 
            colCategory.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colCategory.DataPropertyName = "Text";
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            colCategory.DefaultCellStyle = dataGridViewCellStyle2;
            colCategory.HeaderText = "Mina Kategorier";
            colCategory.MinimumWidth = 8;
            colCategory.Name = "colCategory";
            // 
            // btnRemove
            // 
            btnRemove.Location = new Point(335, 297);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(112, 34);
            btnRemove.TabIndex = 1;
            btnRemove.Text = "Ta bort";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.MouseClick += btnRemove_MouseClick;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(3, 297);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(112, 34);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Lägg till";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.MouseClick += btnAdd_MouseClick;
            // 
            // tbCategory
            // 
            tbCategory.Location = new Point(3, 234);
            tbCategory.Name = "tbCategory";
            tbCategory.PlaceholderText = "Ny kategori";
            tbCategory.Size = new Size(225, 31);
            tbCategory.TabIndex = 3;
            tbCategory.TextChanged += tbCategory_TextChanged;
            // 
            // lblError
            // 
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(234, 231);
            lblError.Name = "lblError";
            lblError.Size = new Size(213, 63);
            lblError.TabIndex = 4;
            lblError.Text = "placeholder error text";
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(160, 297);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(112, 34);
            btnEdit.TabIndex = 5;
            btnEdit.Text = "Redigera";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.MouseClick += btnEdit_MouseClick;
            // 
            // CategoryPanel
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnEdit);
            Controls.Add(lblError);
            Controls.Add(tbCategory);
            Controls.Add(btnAdd);
            Controls.Add(btnRemove);
            Controls.Add(dgvCategories);
            Name = "CategoryPanel";
            Size = new Size(450, 334);
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvCategories;
        private Button btnRemove;
        private Button btnAdd;
        private TextBox tbCategory;
        private Label lblError;
        private Button btnEdit;
        private DataGridViewTextBoxColumn colCategory;
    }
}
