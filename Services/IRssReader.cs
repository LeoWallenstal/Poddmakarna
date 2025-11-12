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
        public Task<Podcast?> GetPodcastFromRssAsync(string rssUrl);
    }
}
