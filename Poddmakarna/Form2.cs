using Models;
using Services;
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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            RssReader rssReader = new RssReader();
            Podcast podcast = rssReader.GetPodcastFromRssAsync("https://feed.pod.space/ursakta").Result;
            for (int i = 0; i < 5; i++)
            {
                PodCard podCard = new PodCard(podcast);
                flpMyPods.Controls.Add(podCard);
                podCard.MouseClick += PodCard_Clicked;
            }              

        }

        public void PodCard_Clicked(object sender, EventArgs e)
        {
            MessageBox.Show("Form2 has been notified that PodCard was clicked!");
        }
    }
}
