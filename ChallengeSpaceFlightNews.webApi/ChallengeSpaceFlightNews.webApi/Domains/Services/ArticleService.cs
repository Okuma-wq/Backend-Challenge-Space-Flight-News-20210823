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
            return await _articleRepository.BuscarPorId(id);
        }

        public async Task<bool> Deletar(string id)
        {
            var article = await _articleRepository.BuscarPorId(id);
            if (article == null)
                return false;

            await _articleRepository.Deletar(article);
            return true;
        }
    }
}
