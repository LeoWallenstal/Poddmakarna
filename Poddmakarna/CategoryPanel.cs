using BL;
using Models;
using System.ComponentModel;


namespace UI
{
    public partial class CategoryPanel : UserControl
    {
        public event Action<Category>? OnCategoryAdded;
        public event Action<Category>? OnCategoryRemoved;
        public event Action<Category>? OnCategoryTextChanged;


        private readonly ICategoryService categoryService;
        private BindingList<Category> _categories;
        private string? _originalCategoryText;

        public CategoryPanel(ICategoryService categoryService)
        {
            InitializeComponent();
            this.categoryService = categoryService;
            InitCategoryTable();

            btnRemove.Enabled = false;
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            lblError.Visible = false;
        }

        private async void InitCategoryTable()
        {
            List<Category> listCategories = await categoryService.GetAllAsync();
            _categories = new BindingList<Category>(listCategories);

            dgvCategories.AutoGenerateColumns = false;
            dgvCategories.DataSource = _categories;
        }

        private void dgvCategories_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Om användaren klickar på kolumnen t.ex, tror jag
            if (e.RowIndex < 0) return;

            btnRemove.Enabled = true;
            btnEdit.Enabled = true;
        }

        private void btnRemove_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvCategories.CurrentRow == null) return;

            Category currentCategory = (Category)dgvCategories.CurrentRow.DataBoundItem;
            DialogResult dialogResult = MessageBox.Show($"Vill du ta bort {currentCategory.Text}?",
                "Ta bort kategori", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                _categories.Remove(currentCategory);
                OnCategoryRemoved?.Invoke(currentCategory);
            }
        }

        private void dgvCategories_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvCategories.ClearSelection();
        }

        private void tbCategory_TextChanged(object sender, EventArgs e)
        {
            if (lblError.Visible)
            {
                lblError.Visible = false;
            }
            btnAdd.Enabled = tbCategory.Text.Length > 0;
        }

        private async void btnAdd_MouseClick(object sender, MouseEventArgs e)
        {
            bool categoryExists = await categoryService.CategoryExistsAsync(tbCategory.Text);
            if (categoryExists)
            {
                lblError.Text = $"Kategorin \"{tbCategory.Text}\" finns redan!";
                lblError.Visible = true;
            }
            else
            {
                Category aCategory = new Category
                {
                    Text = tbCategory.Text,
                };
                //Lägger till i UI:t
                _categories.Add(aCategory);
                OnCategoryAdded?.Invoke(aCategory);
            }
        }

        private void btnEdit_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvCategories.CurrentCell != null)
            {
                dgvCategories.BeginEdit(true);
            }
        }

        private void dgvCategories_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            Category editedCategory = _categories[e.RowIndex];
            if(editedCategory.Text == null)
            {
                editedCategory.Text = _originalCategoryText;
                return;
            }
            if (_originalCategoryText != editedCategory.Text)
            {
                OnCategoryTextChanged?.Invoke(editedCategory);
            }
        }

        private void dgvCategories_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _originalCategoryText = 
                dgvCategories.Rows[e.RowIndex]
                .Cells[e.ColumnIndex]
                .Value.ToString();
        }
    }
}
