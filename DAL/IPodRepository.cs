using Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IPodRepository : IRepository<Podcast>
    {
        Task<bool> RssExistsAsync(string rssUrl);
        Task<bool> UpdateTitleAsync(Podcast podcast, string title);
        Task<bool> UpdateCategoryAsync(Podcast podcast, ObjectId categoryId);
        Task<List<Podcast>> GetByCategoryAsync(ObjectId categoryId);
    }
}
