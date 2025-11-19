using Models;
using MongoDB.Bson;
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
    public partial class PodPanel : UserControl
    {
        public Podcast Podcast { get; }
        public string PodTitle
        {
            get { return rtbPodTitle.Text; }
        }

        public event EventHandler OnTitleChanged;
        public event Action<Podcast> OnCategoryChanged;

        //private Dictionary<ObjectId, Category> _categoryDict;
        private BindingList<Category> categoryDataSource; //Bound with Form2 ComboBox-source

        public PodPanel(Podcast podcast, BindingList<Category> dataSource)
        {
            Podcast = podcast;
            categoryDataSource = new BindingList<Category>();
            UpdateDataSource(dataSource);

            InitializeComponent();
            ClearLabels();
            LoadPodcast();
            LoadCategories();

            dgvEpisodes.CellMouseClick += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                Episode epClicked = podcast.Episodes.ElementAt(e.RowIndex);
                ShowEpisode(epClicked);
            };

            //Nånting med att trolla bort carets på RichTextBoxes som är readonly
            //Just nu har rtbPodDesc och rtbEpDesc en 'I'-caret
        }

        public void UpdateComboBoxSelection() { 
            cbCategories.SelectedItem = categoryDataSource.First(c => c.Id == Podcast.Category);
        }

        public void UpdateDataSource(BindingList<Category> listToCopy) {
            BindingList<Category> copy = new BindingList<Category>(
                listToCopy.Select(c => new Category {
                     Id = c.Id,
                     Text = c.Text
                 }).ToList());
            categoryDataSource = copy;
            categoryDataSource[0].Text = "Okategoriserad"; //Fixar dummy itemet som ligger först
            categoryDataSource.ResetBindings();
        }

        private void ClearLabels()
        {
            lblEpDate.Text = "";
            rtbEpDesc.Text = "";
            lblEpTitle.Text = "";
            lblEpDuration.Text = "";
        }

        private void LoadPodcast()
        {
            rtbPodTitle.Text = Podcast.Title;
            rtbPodDesc.Text = Podcast.Description;
            pbThumbnail.ImageLocation = Podcast.ImageUrl;
            pbThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;

            dgvEpisodes.AutoGenerateColumns = false;
            dgvEpisodes.DataSource = Podcast.Episodes;

            ShowEpisode(Podcast.Episodes.First());
        }

        private void LoadCategories()
        {
            cbCategories.Items.Clear();
            cbCategories.DataSource = categoryDataSource;
            if (categoryDataSource.Any(c => c.Id == Podcast.Category))
            {
                cbCategories.SelectedItem = categoryDataSource.First(c => c.Id == Podcast.Category);
            }

            //Hanterar ändring av kategori på en podcast
            cbCategories.SelectedIndexChanged += (s, e) =>
            {
                if (cbCategories.SelectedItem is Category selectedCategory)
                {
                    if (Podcast.Category != selectedCategory.Id)
                    {
                        Podcast.Category = selectedCategory.Id;
                        OnCategoryChanged(Podcast);
                    }
                }
            };
        }

        private void ShowEpisode(Episode anEpisode)
        {
            lblEpTitle.Text = anEpisode.Title;
            rtbEpDesc.Text = anEpisode.Description;
            lblEpDate.Text = anEpisode.PublishedDate;
            lblEpDuration.Text = anEpisode.Duration;
        }

        private void rtbPodTitle_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtbPodTitle.Text))
            {
                //If the RTB is empty, fallback to previous Title
                rtbPodTitle.Text = Podcast.Title;
            }
            else if (rtbPodTitle.Text != Podcast.Title)
            {
                Podcast.Title = rtbPodTitle.Text;
                OnTitleChanged.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
