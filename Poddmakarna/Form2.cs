using BL;
using DAL;
using Models;
using Services;
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
    public partial class Form2 : Form
    {
        private readonly IPodService _podService;
        private Podcast selectedPodcast;
        public Form2(IPodService podService)
        {
            InitializeComponent();
            this.Load += LoadPodcast;
            _podService = podService;
            btnSave.Hide();
        }

        private async void LoadPodcast(object sender, EventArgs e)
        {
            //RssReader rssReader = new RssReader();
            //Podcast? podcast = await rssReader.GetPodcastFromRssAsync("https://feed.pod.space/ursakta");

            List<Podcast> allaPoddar = await _podService.GetAllAsync();

            foreach (Podcast pod in allaPoddar)
            {
                PodCard podCard = new PodCard(pod);
                flpMyPods.Controls.Add(podCard);
                podCard.MouseClick += PodCard_Clicked;
            }

            if (allaPoddar.Count > 0)
                DisplayPodPanel(allaPoddar.First());
        }

        public void PodCard_Clicked(object sender, EventArgs e)
        {
            if (sender is PodCard podCard)
            {
                Debug.WriteLine(flpMyPods.Controls.IndexOf(podCard));
                DisplayPodPanel(podCard.Podcast);
            }
        }

        private async void DisplayPodPanel(Podcast podcast)
        {
            pPodPanel.Controls.Clear();
            pPodPanel.Controls.Add(new PodPanel(podcast));
            selectedPodcast = podcast;

            if (await _podService.RssExistsAsync(podcast.RssUrl))
            {
                btnSave.Hide();
                btnDelete.Show();
            }
            else
            {
                btnSave.Show();
                btnDelete.Hide();
            }
        }

        private async void btnGetRss_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Podcast? pendingPodcast = await _podService.FetchPodFromRssAsync(tbRssUrl.Text);
            if (pendingPodcast != null)
            {
                DisplayPodPanel(pendingPodcast);
            }
            this.Cursor = Cursors.Default;

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (selectedPodcast != null)
            {
                await _podService.InsertAsync(selectedPodcast);
                btnSave.Hide();
                btnDelete.Show();
                PodCard podCard = new PodCard(selectedPodcast);
                flpMyPods.Controls.Add(podCard);
                podCard.MouseClick += PodCard_Clicked;
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if(selectedPodcast != null)
            {
                await _podService.DeleteAsync(selectedPodcast);
                btnDelete.Hide();
                btnSave.Show();
                foreach (var card in flpMyPods.Controls.OfType<PodCard>().ToList())
                {
                    if (card.Podcast.RssUrl == selectedPodcast.RssUrl)
                        flpMyPods.Controls.Remove(card);
                }
            }
        }
    }
}
