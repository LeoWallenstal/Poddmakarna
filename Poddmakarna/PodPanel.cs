using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class PodPanel : UserControl
    {
        private readonly Podcast _podcast;
        public PodPanel(Podcast podcast)
        {
            _podcast = podcast;
            InitializeComponent();
            ClearLabels();
            LoadPodcast();

            dgvEpisodes.CellMouseClick += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                Episode epClicked = podcast.Episodes.ElementAt(e.RowIndex);
                ShowEpisode(epClicked);
            };

            //Nånting med att trolla bort carets på RichTextBoxes som är readonly
            //Just nu har rtbPodDesc och rtbEpDesc en 'I'-caret
        }

        private void ClearLabels()
        {
            lblEpDate.Text = "";
            rtbEpDesc.Text = "";
            lblEpTitle.Text = "";
        }

        private void LoadPodcast()
        {
            rtbPodTitle.Text = _podcast.Title;
            rtbPodDesc.Text = _podcast.Description;
            pbThumbnail.ImageLocation = _podcast.ImageUrl;
            pbThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;

            dgvEpisodes.AutoGenerateColumns = false;
            dgvEpisodes.DataSource = _podcast.Episodes;

            ShowEpisode(_podcast.Episodes.First());
        }

        private void ShowEpisode(Episode anEpisode)
        {
            lblEpTitle.Text = anEpisode.Title;
            rtbEpDesc.Text = anEpisode.Description;
            lblEpDate.Text = anEpisode.PublishedDate;
        }
    }
}
