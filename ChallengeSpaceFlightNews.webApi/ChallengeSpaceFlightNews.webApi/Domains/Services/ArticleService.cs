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

        public async Task<IEnumerable<Article>> ListarArticlesAsync(int? inicial, int? qtd)
        {
            var initial = inicial ?? 0;
            var quantidade = qtd ?? 5;
            return await _articleRepository.ListarArticlesAsync(initial, quantidade);
        }

        public async Task<Article> CadastrarAsync(CriarArticleDTO novoArticle)
        {
            var articleMapeado = _mapper.Map<Article>(novoArticle);

            await _articleRepository.CadastrarAsync(articleMapeado);

            return articleMapeado;
        }

        public async Task<Article> BuscarPorId(int id)
        {
            return await _articleRepository.BuscarPorIdAsync(id);
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var article = await _articleRepository.BuscarPorIdAsync(id);
            if (article == null)
                return false;

            await _articleRepository.Deletar(article);
            return true;
        }

        public async Task<Article> AlterarAsync(int id, AlterarArticleDTO alteracoes)
        {
            var articleNoBanco = await _articleRepository.BuscarPorIdAsync(id);
            if (articleNoBanco == null)
                return null;

            var articleMapeado = _mapper.Map<Article>(alteracoes);
            articleMapeado.Id = id;
            articleMapeado.UpdatedAt = DateTime.Now;
            articleMapeado.PublishedAt = articleNoBanco.PublishedAt;

            await _articleRepository.AlterarAsync(articleMapeado);
            return articleMapeado;
        }
    }
}
