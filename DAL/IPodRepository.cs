using Models;
using MongoDB.Bson;

namespace DAL
{
    public interface IPodRepository : IRepository<Podcast>
    {
        Task<bool> RssExistsAsync(string rssUrl);
        Task<bool> UpdateTitleAsync(Podcast podcast, string title);
        Task<bool> UpdateCategoryAsync(Podcast podcast, ObjectId categoryId);
        Task<List<Podcast>> GetByCategoryAsync(ObjectId categoryId);
        Task<bool> UpdateNewEpisodes(Podcast toUpdate, List<Episode> newEpisodes);
    }
}
