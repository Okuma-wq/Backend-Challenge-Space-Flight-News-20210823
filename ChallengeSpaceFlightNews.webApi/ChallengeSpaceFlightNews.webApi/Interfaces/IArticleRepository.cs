using ChallengeSpaceFlightNews.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSpaceFlightNews.webApi.Interfaces
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> ListarArticlesAsync(int inicial, int qtd);
        Task CadastrarAsync(Article article);
        Task<Article> BuscarPorIdAsync(int id);
        Task Deletar(Article article);
        Task AlterarAsync(Article article);
    }
}
