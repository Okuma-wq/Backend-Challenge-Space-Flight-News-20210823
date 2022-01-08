using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSpaceFlightNews.webApi.Domains
{
    public class Event
    {
        public string Id { get; set; }
        public string Provider { get; set; }

        public string ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
