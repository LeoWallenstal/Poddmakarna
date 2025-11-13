using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;
using Services;

namespace BL
{
    public class PodcastService : EntityService<Podcast>, IPodService
    {
        private readonly IRssReader _rssReader;
        public PodcastService(EntityRepository<Podcast> repository, IRssReader rssReader) : base(repository){
            _rssReader = rssReader;
        }

        public async Task<Podcast?> FetchPodFromRss(string rssUrl)
        {
            return await _rssReader.GetPodcastFromRssAsync(rssUrl);
        }

        public Task<List<Podcast>> GetByCategoryAsync(string categoryId)
        {
            throw new NotImplementedException(); //Fixa senare : )
        }
    }
}
