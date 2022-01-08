using AutoBogus;
using ChallengeSpaceFlightNews.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceFlightNewsTestes.Integracao.Fakers
{
    public class ArticleBuilder : AutoFaker<Article>
    {
        public ArticleBuilder()
        {
            RuleFor(x => x.Id, faker => faker.Random.Number(1000).ToString());
            RuleFor(x => x.Title, faker => faker.Name.JobTitle());
            RuleFor(x => x.Url, faker => faker.Internet.Url());
            RuleFor(x => x.ImageUrl, faker => faker.Image.PicsumUrl());
            RuleFor(x => x.NewsSite, faker => faker.Internet.DomainWord());
            RuleFor(x => x.Summary, faker => faker.Lorem.Paragraphs(1));
            RuleFor(x => x.PublishedAt, faker => faker.Date.Past(2));
            RuleFor(x => x.UpdatedAt, faker => faker.Date.Past(1));
            RuleFor(x => x.Featured, faker => false);
            RuleFor(x => x.Launches, faker => new List<Launch>());
            RuleFor(x => x.Events, faker => new List<Event>());
        }
    }
}
