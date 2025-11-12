using DAL;
using Models;
using MongoDB.Driver;
using Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Poddmakarna
{
    public partial class Form1 : Form
    {
        private readonly MongoDbContext _dbContext;
        private readonly PodRepository podcastRepo;
        public Form1()
        {
            InitializeComponent();
            _dbContext = new MongoDbContext();
            podcastRepo = new PodRepository(_dbContext.Database.GetCollection<Podcast>("Pods"));
        }

        private async void btnGetPodcast_Click(object sender, EventArgs e)
        {
            string url = txtBoxURL.Text;
            IRssReader rssReader = new RssReader();
            Podcast? podcast = await rssReader.GetPodcastFromRssAsync(url);
            if (podcast == null) return;

            label1.Text = podcast.Title;
            await podcastRepo.InsertAsync(podcast);
        }
    }
}
