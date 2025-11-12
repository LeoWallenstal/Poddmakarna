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
           
                try
                {
                return await Task.Run(() =>
                {
                    XmlReader xmlReader = XmlReader.Create(rssUrl);
                    SyndicationFeed feed = SyndicationFeed.Load(xmlReader);

                    Podcast podcast = new Podcast
                    {
                        Title = feed.Title.Text ?? "No Title",
                        Description = feed.Description.Text,
                        ImageUrl = feed.ImageUrl?.ToString() ?? string.Empty,

                    };
                    return podcast;
                    });
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error)
                    Debug.WriteLine($"Error fetching RSS feed: {ex.Message}");
                    return null;
                }
            
        }
    }
}
