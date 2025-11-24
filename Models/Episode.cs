

namespace Models
{
    public class Episode
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset PublishedDate { get; set; }
        public string Duration { get; set; }

    }
}
