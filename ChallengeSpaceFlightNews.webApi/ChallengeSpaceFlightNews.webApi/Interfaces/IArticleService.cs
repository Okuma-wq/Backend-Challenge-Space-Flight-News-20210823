using ChallengeSpaceFlightNews.webApi.Domains;
using ChallengeSpaceFlightNews.webApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSpaceFlightNews.webApi.Interfaces
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> ListarArticlesAsync(int? inicial, int? qtd);
        Task<Article> CadastrarAsync(CriarArticleDTO novoArticle);
        Task<Article> BuscarPorId(int id);
        Task<bool> DeletarAsync(int id);
        Task<Article> AlterarAsync(int id, AlterarArticleDTO alteracoes);
    }
}
