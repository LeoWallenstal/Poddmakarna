using MongoDB.Bson;

namespace Models
{
    public class Podcast : BaseEntity
    {
        //Id ärvs from BaseEntity
        public string Title { get; set; } //Validering i setters
        
        //Sätts av användaren, om de vill
        public string? CustomTitle { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string ImageUrl { get; set; }

        public ObjectId Category { get; set; }

        public List<Episode> Episodes { get; set; } = new();
        public string RssUrl { get; set; }

        public Podcast() : base() { //Ska man ha flera konstruktorer här?? (MongoDB osv)
        
        }
    }
}
