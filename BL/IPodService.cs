using Models;
using MongoDB.Bson;

namespace BL
{
    public interface IPodService : IService<Podcast>
    {
        Task<List<Podcast>> GetByCategoryAsync(ObjectId categoryId);
        Task<Podcast?> FetchPodFromRssAsync(string rssUrl);
        Task<bool> RssExistsAsync(string rssUrl);
        Task<bool> UpdateTitleAsync(Podcast toUpdate, string newTitle);
        Task<bool> UpdateCategoryAsync(Podcast toUpdate, ObjectId newCategory);

        Task<List<Podcast>> FetchNewEpisodes(List<Podcast> toUpdate);
    }
}
