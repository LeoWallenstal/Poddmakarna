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
        public Podcast Podcast { get; }
        public string PodTitle
        {
            get { return rtbPodTitle.Text; }
        }

        public event EventHandler OnTitleChanged;

        public PodPanel(Podcast podcast)
        {
            Podcast = podcast;
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
