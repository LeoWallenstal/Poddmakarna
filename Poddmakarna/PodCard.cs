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
    public partial class PodCard : UserControl
    {
        public Podcast Podcast { get; }
        public Label TitleLabel {
            get { return lblTitle; }
        }
        public PodCard(Podcast podcast)
        {
            Podcast = podcast;
            InitializeComponent();
            LoadPodCard();

            foreach(Control c in this.Controls)
            {
                c.MouseClick += (s, e) => 
                {
                    this.OnMouseClick(e);
                };
            }   
        }

        private void LoadPodCard()
        {
            lblTitle.Text = $"{Podcast.Title}";
            Debug.WriteLine("Loading PodCard for podcast: " + Podcast.Title);
            //Kategorier
            pbThumbnail.ImageLocation = Podcast.ImageUrl;
            pbThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
