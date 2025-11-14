using Models;
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
    }
}
