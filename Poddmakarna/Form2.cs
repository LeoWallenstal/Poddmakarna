using DAL;
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
        private readonly MongoDbContext _dbContext;
        private readonly PodRepository _podRepo;
        public Form2()
        {
            InitializeComponent();
            this.Load += LoadPodcast;
            _dbContext = new MongoDbContext();
            _podRepo = new PodRepository(_dbContext.Database.GetCollection<Podcast>("Pods"));

        }

        private async void LoadPodcast(object sender, EventArgs e)
        {
            //RssReader rssReader = new RssReader();
            //Podcast? podcast = await rssReader.GetPodcastFromRssAsync("https://feed.pod.space/ursakta");

            List<Podcast> allaPoddar = await _podRepo.GetAllAsync();

            foreach(Podcast pod in allaPoddar)
            {
                PodCard podCard = new PodCard(pod);
                flpMyPods.Controls.Add(podCard);
                podCard.MouseClick += PodCard_Clicked;
            }
        }

        public void PodCard_Clicked(object sender, EventArgs e)
        {
            if (sender is PodCard podCard)
            {
                pPodPanel.Controls.Clear();
                pPodPanel.Controls.Add(new PodPanel(podCard.Podcast));
            }
        }
    }
}
