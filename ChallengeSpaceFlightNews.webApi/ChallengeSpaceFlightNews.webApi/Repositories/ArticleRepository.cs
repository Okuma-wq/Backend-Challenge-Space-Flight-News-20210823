using ChallengeSpaceFlightNews.webApi.Data;
using ChallengeSpaceFlightNews.webApi.Domains;
using ChallengeSpaceFlightNews.webApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ChallengeSpaceFlightNews.webApi.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly SpaceFlightNewsContext _context;

        public ArticleRepository(SpaceFlightNewsContext contexto)
        {
            _context = contexto;
        }

        public async Task AlterarAsync(Article article)
        {
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
        }

        public async Task<Article> BuscarPorIdAsync(int id)
        {
            return await _context.Articles
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CadastrarAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Article article)
        {
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Article>> ListarArticlesAsync(int inicial, int qtd)
        {
            return await _context.Articles
                .AsNoTracking()
                .Skip(inicial).Take(qtd)
                .ToListAsync();
        }
    }
}
