using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Linq.Expressions;
using System.Diagnostics;

namespace Services
{
    public class RssReader : IRssReader
    {
        public async Task<Podcast?> GetPodcastFromRssAsync(string rssUrl)
        {
            return await Task.Run(() =>
            {
                try
                {
                    XmlReader xmlReader = XmlReader.Create(rssUrl);
                    SyndicationFeed rssFeed = SyndicationFeed.Load(xmlReader);

                    Podcast podcast = new Podcast
                    {
                        Title = rssFeed.Title.Text ?? "No Title",
                        Description = rssFeed.Description.Text ?? "No description",
                        ImageUrl = rssFeed.ImageUrl?.ToString() ?? string.Empty,
                        Episodes = FetchEpisodesFromRssAsync(rssFeed).Result
                    };
                    return podcast;
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error)
                    Debug.WriteLine($"Error fetching RSS feed: {ex.Message}");
                    return null;
                }
            });
        }

        //Denna borde inte vara public känns det som, men vi tänker på det sen : )
        public async Task<List<Episode>> FetchEpisodesFromRssAsync(SyndicationFeed rssFeed) {
            List<Episode> allEpisodes = new List<Episode>();

            return await Task.Run(() =>
            {
                foreach (SyndicationItem anEpisode in rssFeed.Items)
                {
                    Episode episode = new Episode
                    {
                        Title = anEpisode.Title.Text ?? "No Title",
                        Description = anEpisode.Summary.Text ?? "No Description",
                        PublishedDate = anEpisode.PublishDate.ToString("yyyy-MM-dd HH:ss") ?? "No published date",
                    };
                    allEpisodes.Add(episode);
                }
                return allEpisodes;
            });
        }
    }
}
