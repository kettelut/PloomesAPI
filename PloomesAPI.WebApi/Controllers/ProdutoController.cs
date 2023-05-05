using Microsoft.AspNetCore.Mvc;
using PloomesAPI.Application.Interfaces;
using PloomesAPI.Domain.Entities;

namespace PloomesAPI.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ILogger<ProdutoController> _logger;
        private readonly IProdutoApp _produtoApp;

        public ProdutoController(ILogger<ProdutoController> logger, IProdutoApp produtoApp)
        {
            _logger = logger;
            _produtoApp = produtoApp;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Produto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromQuery] bool? ativo)
        {
            List<Produto> _listaProduto = _produtoApp.ObterTodos(ativo);

            if (_listaProduto.Count == 0)
                return NotFound();
            else
                return Ok(_listaProduto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Produto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            if (_produtoApp.Existe(id))
                return Ok(_produtoApp.ObterPelaPK(id));
            else
                return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Produto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Produto produto)
        {
            if (produto == null)
                return BadRequest();

            return Ok(_produtoApp.Cadastrar(produto));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Produto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] Produto produto)
        {
            if (produto == null)
                return BadRequest();

            return Ok(_produtoApp.Atualizar(produto));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            _produtoApp.Deletar(id);
            return NoContent();
        }
    }
}