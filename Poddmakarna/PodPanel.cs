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
        }

        private void ClearLabels()
        {
            lblEpDate.Text = "";
            lblEpDesc.Text = "";
            lblEpTitle.Text = "";
        }

        private void LoadPodcast()
        {
            rtbPodTitle.Text = _podcast.Title;
            rtbPodDesc.Text = _podcast.Description;
            pbThumbnail.ImageLocation = _podcast.ImageUrl;
            pbThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
