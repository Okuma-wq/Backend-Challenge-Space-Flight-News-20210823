﻿using ChallengeSpaceFlightNews.webApi.Data;
using ChallengeSpaceFlightNews.webApi.Domains;
using ChallengeSpaceFlightNews.webApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSpaceFlightNews.webApi.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly SpaceFlightNewsContext _context;

        public ArticleRepository(SpaceFlightNewsContext contexto)
        {
            _context = contexto;
        }

        public async Task CadastrarAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Article>> ListarArticlesAsync()
        {
            return await _context.Articles
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
