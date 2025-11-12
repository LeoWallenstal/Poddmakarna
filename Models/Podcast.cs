using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Podcast
    {
        //Id
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Categories { get; set; }
        //public List<Episode> Episodes { get; set; }

    }
}
