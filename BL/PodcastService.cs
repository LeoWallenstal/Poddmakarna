using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;
using MongoDB.Bson;
using Services;

namespace BL
{
    public class PodcastService : EntityService<Podcast, IPodRepository>, IPodService
    {
        private readonly IRssReader _rssReader;
        //Kolla om detta är en bra lösning??
        public PodcastService(IPodRepository repository, IRssReader rssReader) : base(repository){
            _rssReader = rssReader;
        }

        public async Task<Podcast?> FetchPodFromRssAsync(string rssUrl)
        {
            return await _rssReader.GetPodcastFromRssAsync(rssUrl);
        }

        public async Task<List<Podcast>> GetByCategoryAsync(ObjectId categoryId)
        {
            return await repository.GetByCategoryAsync(categoryId);
        }

        public async Task<bool> RssExistsAsync(string rssUrl)
        {
           return await repository.RssExistsAsync(rssUrl);
        }

        public async Task<bool> UpdateCategoryAsync(Podcast toUpdate, ObjectId newCategory)
        {
            return await repository.UpdateCategoryAsync(toUpdate, newCategory);
        }

        public async Task<bool> UpdateTitleAsync(Podcast toUpdate, string newTitle)
        {
            return await repository.UpdateTitleAsync(toUpdate, newTitle);
        }
    }
}
