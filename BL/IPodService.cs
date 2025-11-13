using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using Models;
using Services;

namespace BL
{
    public interface IPodService : IService<Podcast>
    {
        Task<List<Podcast>> GetByCategoryAsync(string categoryId);
        Task<Podcast?> FetchPodFromRss(string rssUrl);
    }
}
