﻿using ChallengeSpaceFlightNews.webApi.Domains;
using ChallengeSpaceFlightNews.webApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSpaceFlightNews.webApi.Interfaces
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> ListarArticlesAsync();
        Task<Article> CadastrarAsync(CriarArticleDTO novoArticle);
    }
}