using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using Models;
using MongoDB.Bson;
using Services;

namespace BL
{
    public interface IPodService : IService<Podcast>
    {
        Task<List<Podcast>> GetByCategoryAsync(ObjectId categoryId);
        Task<Podcast?> FetchPodFromRssAsync(string rssUrl);
        Task<bool> RssExistsAsync(string rssUrl);
        Task<bool> UpdateTitleAsync(Podcast toUpdate, string newTitle);
        Task<bool> UpdateCategoryAsync(Podcast toUpdate, ObjectId newCategory);
    }
}
