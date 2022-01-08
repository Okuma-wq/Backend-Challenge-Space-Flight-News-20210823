using AutoBogus;
using ChallengeSpaceFlightNews.webApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceFlightNewsTestes.Integracao.Fakers
{
    public class CriarArticleDTOBuilder : AutoFaker<CriarArticleDTO>
    {
        public CriarArticleDTOBuilder()
        {
            RuleFor(x => x.Title, faker => faker.Commerce.ProductName());
            RuleFor(x => x.Url, faker => faker.Internet.Url());
            RuleFor(x => x.ImageUrl, faker => faker.Image.PicsumUrl());
            RuleFor(x => x.NewsSite, faker => faker.Company.CompanyName());
            RuleFor(x => x.Summary, faker => faker.Lorem.Paragraph(1));
            RuleFor(x => x.Featured, faker => faker.PickRandomParam(true, false));
        }
    }
}
