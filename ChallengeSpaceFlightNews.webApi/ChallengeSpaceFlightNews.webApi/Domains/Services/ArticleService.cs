using AutoMapper;
using ChallengeSpaceFlightNews.webApi.DTOs;
using ChallengeSpaceFlightNews.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSpaceFlightNews.webApi.Domains.Services
{
    public class ArticleService : IArticleService
    {
        private IArticleRepository _articleRepository { get; }
        private IMapper _mapper { get; }


        public ArticleService(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Article>> ListarArticlesAsync()
        {
            return await _articleRepository.ListarArticlesAsync();
        }

        public async Task<Article> CadastrarAsync(CriarArticleDTO novoArticle)
        {
            var articleMapeado = _mapper.Map<Article>(novoArticle);

            articleMapeado.Id = ((await _articleRepository.ListarArticlesAsync()).Count() + 1).ToString();

            await _articleRepository.CadastrarAsync(articleMapeado);

            return articleMapeado;
        }

        public async Task<Article> BuscarPorId(string id)
        {
            return await _articleRepository.BuscarPorIdAsync(id);
        }

        public async Task<bool> DeletarAsync(string id)
        {
            var article = await _articleRepository.BuscarPorIdAsync(id);
            if (article == null)
                return false;

            await _articleRepository.Deletar(article);
            return true;
        }

        public async Task<Article> AlterarAsync(string id, AlterarArticleDTO articleAlterado)
        {
            var articleNoBanco = await _articleRepository.BuscarPorIdAsync(id);
            if (articleNoBanco == null)
                return null;

            var articleMapeado = _mapper.Map<Article>(articleAlterado);
            articleMapeado.Id = id;
            articleMapeado.UpdatedAt = DateTime.Now;
            articleMapeado.PublishedAt = articleNoBanco.PublishedAt;

            await _articleRepository.AlterarAsync(articleMapeado);
            return articleMapeado;
        }
    }
}
