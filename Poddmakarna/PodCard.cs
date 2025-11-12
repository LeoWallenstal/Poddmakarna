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
        private readonly Podcast _podcast;
        public PodCard(Podcast podcast)
        {
            _podcast = podcast;
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
            lblTitle.Text = _podcast.Title;
            //Kategorier
            pbThumbnail.ImageLocation = _podcast.ImageUrl;
            pbThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
