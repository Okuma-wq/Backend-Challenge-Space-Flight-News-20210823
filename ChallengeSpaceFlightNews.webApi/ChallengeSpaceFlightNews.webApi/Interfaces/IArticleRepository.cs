using ChallengeSpaceFlightNews.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSpaceFlightNews.webApi.Interfaces
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> ListarArticlesAsync();
        Task CadastrarAsync(Article article);
        Task<Article> BuscarPorId(string id);
        Task Deletar(Article article);
    }
}
