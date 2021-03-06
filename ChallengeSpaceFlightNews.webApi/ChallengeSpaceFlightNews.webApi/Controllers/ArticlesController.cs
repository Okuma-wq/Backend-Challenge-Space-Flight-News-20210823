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
        public async Task<ActionResult<IEnumerable<Article>>> Listar(int? inicial, int? qtd)
        {
            return Ok( await _articleService.ListarArticlesAsync(inicial, qtd)); ;
        }

        [HttpPost]
        public async Task<ActionResult<Article>> Cadastrar(CriarArticleDTO novoArticle)
        {
            novoArticle.Validar();
            if (!novoArticle.IsValid)
                return BadRequest(novoArticle.Notifications);

            var resposta = await _articleService.CadastrarAsync(novoArticle);

            return Created("", resposta);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> BuscarPorId(int id)
        {
            var resposta = await _articleService.BuscarPorId(id);
            if (resposta == null)
                return NotFound("Não foi possivel encontrar um article com o id procurado");

            return Ok(resposta);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarAsync(int id)
        {
            var resposta = await _articleService.DeletarAsync(id);

            if (resposta == false)
                return NotFound("Não foi possivel encontrar um article com o id utilizado");

            return Ok("Article deletado com sucesso!");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Article>> AlterarAsync(int id, AlterarArticleDTO alteracoes)
        {
            alteracoes.Validar();
            if (!alteracoes.IsValid)
                return BadRequest(alteracoes.Notifications);

            var articleRetornado = await _articleService.AlterarAsync(id, alteracoes);
            if (articleRetornado == null)
                return NotFound("O id fornecido não corresponde a nenhum article salvo");

            return Ok(articleRetornado);
        }
    }
}
