using ChallengeSpaceFlightNews.webApi.Domains;
using ChallengeSpaceFlightNews.webApi.DTOs;
using ChallengeSpaceFlightNews.webApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSpaceFlightNews.webApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {

        private IArticleService _articleService { get; }

        public ArticlesController(IArticleService article)
        {
            _articleService = article;
        }


        /// <summary>
        /// Retorna Status code 200 Ok e o título
        /// </summary>
        /// <returns>string</returns>
        [HttpGet("/")]
        public async Task<ActionResult<string>> DefaultMethod()
        {
            return Ok("Back - end Challenge 2021 🏅 -Space Flight News");
        }


        /// <summary>
        /// Retorna uma lista do artigos cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> Listar()
        {
            return Ok( await _articleService.ListarArticlesAsync()); ;
        }

        [HttpPost]
        public async Task<ActionResult<Article>> Cadastrar(CriarArticleDTO novoArticle)
        {
            var resposta = await _articleService.CadastrarAsync(novoArticle);

            return Created("", resposta);
        }
    }
}
