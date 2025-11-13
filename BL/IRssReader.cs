using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.ServiceModel.Syndication;

namespace Services
{
    public interface IRssReader
    {
        Task<Podcast?> GetPodcastFromRssAsync(string rssUrl);
        Task<List<Episode>> FetchEpisodesFromRssAsync(SyndicationFeed rssFeed);
    }
}
