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
using System.Text.RegularExpressions;
using System.Net;
using MongoDB.Bson;

namespace Services
{
    public class RssReader : IRssReader
    {
        private readonly string ItunesNamespace = "http://www.itunes.com/dtds/podcast-1.0.dtd";

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
                        Description = StripHtml(rssFeed.Description.Text) ?? "No description",
                        ImageUrl = rssFeed.ImageUrl?.ToString() ?? string.Empty,
                        Episodes = FetchEpisodesFromRssAsync(rssFeed).Result,
                        RssUrl = rssUrl
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
                //Description laddas inte från vissa
                SyndicationItem e = rssFeed.Items.FirstOrDefault();
                
                foreach (SyndicationItem anEpisode in rssFeed.Items)
                {    

                    Episode episode = new Episode
                    {
                        Title = anEpisode.Title.Text ?? "No Title",
                        Description = StripHtml(GetDescription(anEpisode)),
                        PublishedDate = anEpisode.PublishDate.ToString("yyyy-MM-dd HH:ss") ?? "No published date",
                        Duration = GetDuration(anEpisode)
                    };
                    allEpisodes.Add(episode);
                }
                return allEpisodes;
            });
        }

        private string GetDuration(SyndicationItem anEpisode)
        {
            var iTunesDuration = anEpisode.ElementExtensions
                .ReadElementExtensions<string>("duration", ItunesNamespace)
                .FirstOrDefault();

            return iTunesDuration ?? "";
        }

        private string GetDescription(SyndicationItem anEpisode)
        {
            if (anEpisode.Summary != null)
            { 
                return anEpisode.Summary.Text;
            }


            if (anEpisode.Content is TextSyndicationContent textContent)
            { 
                return textContent.Text;
            }

            var iTunesSummary = anEpisode.ElementExtensions
                .ReadElementExtensions<string>("summary", ItunesNamespace)
                .FirstOrDefault();
            if(iTunesSummary != null)
            { 
                return iTunesSummary;
            }

            //Kortare än description, används i sista fall.
            var iTunesSubtitle = anEpisode.ElementExtensions
               .ReadElementExtensions<string>("subtitle", ItunesNamespace)
               .FirstOrDefault();
            if (iTunesSubtitle != null)
            { 
                return iTunesSubtitle;
            }

            return "";
        }

        private string StripHtml(string content)
        {
            var withoutTags = Regex.Replace(content, "<.*?>", string.Empty);

            var withoutEntities = WebUtility.HtmlDecode(withoutTags);

            return withoutEntities.Trim();
        }
    }
}
