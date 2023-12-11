using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using spoofy.application.Conta.Dto;
using spoofy.application.Conta;

namespace spoofy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly UsuarioService _service = new UsuarioService();

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> CriarConta(UsuarioDto dto)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            await _service.CriarConta(dto);

            return Created($"/Usuario/{dto.Id}", dto);
        }

        [HttpGet("{id}")]
        public IActionResult ObterUsuario(Guid id)
        {
            var result = this._service.ObterUsuario(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("{id}/Favoritar")]
        public async Task<IActionResult> FavoritarMusica(Guid id, FavoritarDto dto)
        {
            await _service.FavoritarMusica(id, dto.idMusica);

            return Ok();
        }

        [HttpPut("{id}/updatename")]
        public IActionResult UpdateUsuario(Guid id, NomeDto dto)
        {
            var result = this._service.ObterUsuario(id);

            if (result == null)
                return NotFound();

            _service.UpdateNomeUsuario(id, dto);
            return Created($"/Usuario/{id}", result);

        }

        [HttpPost("{id}/NovaPlaylist")]
        public async Task<IActionResult> NovaPlaylist(Guid id, PlaylistDto dto)
        {
            var result = this._service.ObterUsuario(id);

            if (result == null)
                return NotFound();

            await _service.NovaPlaylist(id, dto);
            return Created($"/Usuario/{dto.Id}/NovaPlaylist", dto);
        }

        [HttpPost("{id}/Playlist/{idPlaylist}/AdicionarMusica")]
        public async Task<IActionResult> AdicionarMusica(Guid id, Guid idMusica, Guid idPlaylist)
        {
            await _service.AdicionarMusica(id, idMusica, idPlaylist);
            return Ok();

        }

        [HttpPost("{id}/NovaAssinatura")]
        public async Task<IActionResult> NovaAssinatura(Guid id, CompraDto dto)
        {

            await _service.NovaAssinatura(id, dto);

            return Ok();
        }

    }
}
