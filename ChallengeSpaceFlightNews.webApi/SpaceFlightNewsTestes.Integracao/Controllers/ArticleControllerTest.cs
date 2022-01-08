using ChallengeSpaceFlightNews.webApi.Domains;
using ChallengeSpaceFlightNews.webApi.Interfaces;
using FluentAssertions;
using SpaceFlightNewsTestes.Integracao.Fakers;
using SpaceFlightNewsTestes.Integracao.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SpaceFlightNewsTestes.Integracao.Controllers
{
    public class ArticleControllerTest : TestBase
    {
        public IArticleRepository _articleRepository;
        public ArticleControllerTest(ApiWebApplicationFactory factory) : base(factory)
        {
            _articleRepository = (IArticleRepository)Scope.ServiceProvider.GetService(typeof(IArticleRepository));
        }

        private async Task<IEnumerable<Article>> PersistirArticleNoBanco()
        {
            var listaArticles = new ArticleBuilder().Generate(3);
            await Context.Articles.AddRangeAsync(listaArticles);
            await Context.SaveChangesAsync();
            return listaArticles;
        }


        #region Teste de MetodoPadrao

        [Fact]
        public async Task DefaultMethodDeveRetornar200Ok()
        {
            var resposta = await Client.GetAsync("/");

            resposta.Should()
                .Be200Ok();
        }

        [Fact]
        public async Task DefaultMethodDeveRetornarStringEsperada()
        {
            var resposta = await Client.GetFromJsonAsync<string>("/");

            resposta.Should()
                .BeEquivalentTo("Back - end Challenge 2021 🏅 -Space Flight News");
        }

        #endregion

        #region Teste de Listar

        [Fact]
        public async Task ListarDeveRetornarStatusCode200Ok()
        {
            var resposta = await Client.GetAsync("/Articles");

            resposta.Should()
                .Be200Ok();
        }

        [Fact]
        public async Task ListarDeveRetornarArticlesNoBanco()
        {
            var articles = await PersistirArticleNoBanco();

            var resposta = await Client.GetFromJsonAsync<IEnumerable<Article>>("/Articles");

            resposta.Should()
                .BeEquivalentTo(articles);
        }

        #endregion

    }
}
