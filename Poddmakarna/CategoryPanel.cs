using BL;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class CategoryPanel : UserControl
    {
        private readonly ICategoryService categoryService;
        private BindingList<Category> _categories;

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
                categoryService.DeleteAsync(currentCategory);
                _categories.Remove(currentCategory);
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
                //Kanske nån label som säger "bra jobbat : )", eller nått
                Category aCategory = new Category
                {
                    Text = tbCategory.Text,
                };
                //Add to UI
                _categories.Add(aCategory);

                //Add to DB
                await categoryService.InsertAsync(aCategory);
            }
        }

        private void btnEdit_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvCategories.CurrentCell != null)
            {
                dgvCategories.BeginEdit(true);
            }
        }

        private void dgvCategories_Leave(object sender, EventArgs e)
        {
            dgvCategories.ClearSelection();
            btnRemove.Enabled = false;
            btnEdit.Enabled = false;
        }

        private void dgvCategories_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Debug.WriteLine("Hello, EndEdit");
        }

        private void dgvCategories_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvCategories.CurrentCell.ContentBounds.Contains(e.Location)){
                dgvCategories.EndEdit();
                dgvCategories.ClearSelection();
            }
        }
    }
}
