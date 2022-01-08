using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSpaceFlightNews.webApi.Domains
{
    public class Launch
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Provider { get; set; }

        public string ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
