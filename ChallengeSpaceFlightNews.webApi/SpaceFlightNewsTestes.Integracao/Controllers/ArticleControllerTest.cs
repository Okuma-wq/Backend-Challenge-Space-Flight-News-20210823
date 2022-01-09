using ChallengeSpaceFlightNews.webApi.Domains;
using ChallengeSpaceFlightNews.webApi.DTOs;
using ChallengeSpaceFlightNews.webApi.Interfaces;
using FluentAssertions;
using Newtonsoft.Json;
using SpaceFlightNewsTestes.Integracao.Fakers;
using SpaceFlightNewsTestes.Integracao.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            var resposta = await Client.GetAsync("/");

            var conteudo = await resposta.Content.ReadAsStringAsync();

            conteudo.Should()
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
                .BeEquivalentTo(articles, options => options
                    .Using<DateTime>(context => context.Subject.Should()
                    .BeCloseTo(context.Expectation, TimeSpan.FromSeconds(1)))
                    .WhenTypeIs<DateTime>());
        }

        #endregion

        #region Teste de Cadastrar

        [Fact]
        public async Task CadastrarDeveRetornar201CreatedQuandoCadastroForExecutadoCorretamente()
        {
            var article = new CriarArticleDTOBuilder().Generate();

            var resposta = await Client.PostAsJsonAsync("/Articles", article);

            resposta.Should()
                .Be201Created();
        }

        [Fact]
        public async Task CadastrarDeveSalvarArticleNoBanco()
        {
            var article = new CriarArticleDTOBuilder().Generate();

            var resposta = await Client.PostAsJsonAsync("/Articles", article);

            var articleRetornado = await resposta.Content.ReadFromJsonAsync<Article>();

            var articleNoBanco = Context.Articles.FirstOrDefault(x => x.Id == articleRetornado.Id);

            articleRetornado.Should()
                .BeEquivalentTo(articleNoBanco);
        }

        [Fact]
        public async Task CadastrarDeveRetornar400BadRequestSeOsCamposNaoForemPreenchidosCorretamente()
        {
            var articleVazio = new CriarArticleDTO();

            var resposta = await Client.PostAsJsonAsync("/Articles", articleVazio);

            resposta.Should()
                .Be400BadRequest();
        }
        #endregion

        #region Teste BuscarPorId

        [Fact]
        public async Task BuscarPorIdDeveRetornar200OkQuandoArticleForEncontrado()
        {
            var articles = await PersistirArticleNoBanco();

            var article = articles.FirstOrDefault();

            var resposta = await Client.GetAsync($"/Articles/{article.Id}");

            resposta.Should()
                .Be200Ok();
        }

        [Fact]
        public async Task BuscarPorIdDeveRetornarOArticleQueFoiBuscadoNoBanco()
        {
            var articles = await PersistirArticleNoBanco();
            var article = articles.FirstOrDefault();

            var resposta = await Client.GetFromJsonAsync<Article>($"/Articles/{article.Id}");

            resposta.Should()
                .BeEquivalentTo(article, options => options
                    .Using<DateTime>(context => context.Subject.Should()
                    .BeCloseTo(context.Expectation, TimeSpan.FromSeconds(1)))
                    .WhenTypeIs<DateTime>());
        }

        [Fact]
        public async Task BuscarPorIdDeveRetornar404NotFoundSeIdNaoCorresponderAUmArticleSalvoNoBanco()
        {
            var resposta = await Client.GetAsync($"/Articles/1");

            resposta.Should()
                .Be404NotFound();
        }

        #endregion

        #region Teste Deletar

        [Fact]
        public async Task DeletarDeveRetornar200OkSeOArticleForRemovido()
        {
            var articles = await PersistirArticleNoBanco();
            var article = articles.FirstOrDefault();

            var resposta = await Client.DeleteAsync($"/Articles/{article.Id}");

            resposta.Should()
                .Be200Ok();
        }

        [Fact]
        public async Task DeletarDeveRemoverOArticleDoBanco()
        {
            var articles = await PersistirArticleNoBanco();
            var article = articles.FirstOrDefault();

            await Client.DeleteAsync($"/Articles/{article.Id}");

            var retorno = await _articleRepository.BuscarPorIdAsync(article.Id);

            retorno.Should()
                .BeNull();
        }

        [Fact]
        public async Task DeletarDeveRetornar404NotFoundSeOIdUtilizadoNaoCorresponderAUmArticle()
        {
            var resposta = await Client.DeleteAsync($"/Articles/1");

            resposta.Should()
                .Be404NotFound();
        }

        #endregion

        #region Teste Alterar

        [Fact]
        public async Task AlterarDeveRetornar200OkSeOsDadosForemPreenchidosCorretamente()
        {
            var articles = await PersistirArticleNoBanco();
            var article = articles.FirstOrDefault();

            var alteracoes = new AlterarArticleDTOBuilder().Generate();

            var content = JsonConvert.SerializeObject(alteracoes);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var resposta = await Client.PutAsync($"/Articles/{article.Id}", stringContent);

            resposta.Should()
                .Be200Ok();
        }

        [Fact]
        public async Task AlterarDeveAlterarOArticleSalvoNoBanco()
        {
            var articles = await PersistirArticleNoBanco();
            var article = articles.FirstOrDefault();

            var alteracoes = new AlterarArticleDTOBuilder().Generate();

            var content = JsonConvert.SerializeObject(alteracoes);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var resposta = await Client.PutAsync($"/Articles/{article.Id}", stringContent);
            var articleAlterado = await resposta.Content.ReadFromJsonAsync<Article>();

            var articleNobanco = await _articleRepository.BuscarPorIdAsync(article.Id);

            articleNobanco.Should()
                .BeEquivalentTo(articleAlterado, options => options
                    .Using<DateTime>(context => context.Subject.Should()
                    .BeCloseTo(context.Expectation, TimeSpan.FromSeconds(1)))
                    .WhenTypeIs<DateTime>());
        }

        [Fact]
        public async Task AlterarDeveRetornar400BadRequestSeOsDadosNaoForemPreenchidosCorretamente()
        {
            var alteracoes = new AlterarArticleDTO();

            var content = JsonConvert.SerializeObject(alteracoes);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var resposta = await Client.PutAsync($"/Articles/1", stringContent);

            resposta.Should()
                .Be400BadRequest();
        }

        [Fact]
        public async Task AlterarDeveRetornar404NotFoundSeOIdFornecidoNaoCorresponderAUmArticleSalvoNoBanco()
        {
            var alteracoes = new AlterarArticleDTOBuilder().Generate();

            var content = JsonConvert.SerializeObject(alteracoes);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var resposta = await Client.PutAsync($"/Articles/1", stringContent);

            resposta.Should()
                .Be404NotFound();
        }

        #endregion

    }
}
