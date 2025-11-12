using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Podcast : BaseEntity
    {
        //Id from BaseEntity
        public string Title { get; set; } //Validering i setters
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string ImageUrl { get; set; }
        public string Categories { get; set; }
        public List<Episode> Episodes { get; set; } = new();

        public Podcast() : base() { //Ska man ha flera konstruktorer här?? (MongoDB osv)
        
        }
    }
}
