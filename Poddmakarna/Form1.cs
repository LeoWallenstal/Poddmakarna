using Models;
using Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Poddmakarna
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnGetPodcast_Click(object sender, EventArgs e)
        {
            string url = txtBoxURL.Text;
            IRssReader rssReader = new RssReader();
            Podcast podcast = await rssReader.GetPodcastFromRssAsync(url);
            label1.Text = podcast.Title;
        }
    }
}
