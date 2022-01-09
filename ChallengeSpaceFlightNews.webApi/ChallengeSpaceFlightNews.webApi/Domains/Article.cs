using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSpaceFlightNews.webApi.Domains
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string NewsSite { get; set; }
        public string Summary { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Featured { get; set; }


        public IReadOnlyCollection<Event> Events { get { return _events.ToArray(); } }
        private List<Event> _events = new List<Event>();

        public IReadOnlyCollection<Launch> Launches { get { return _launches.ToArray(); } }
        private List<Launch> _launches = new List<Launch>();
    }
}
