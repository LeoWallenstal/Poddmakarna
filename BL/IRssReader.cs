using Models;
using System.ServiceModel.Syndication;

namespace Services
{
    public interface IRssReader
    {
        Task<Podcast?> GetPodcastFromRssAsync(string rssUrl);
        Task<List<Episode>> FetchEpisodesFromRssAsync(SyndicationFeed rssFeed);

        Task<Dictionary<Podcast, List<Episode>>> FetchNewEpisodes(List<Podcast> toUpdate);
    }
}
