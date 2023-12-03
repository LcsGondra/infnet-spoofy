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
        public IActionResult CriarConta(UsuarioDto dto)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            this._service.CriarConta(dto);

            return Created($"/usuario/{dto.Id}", dto);
        }

        [HttpGet("{id}")]
        public IActionResult ObterUsuario(Guid id)
        {
            var result = this._service.ObterUsuario(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        //[HttpPut("{id}")]
        //public IActionResult UpdateUsuario(Guid id, UsuarioDto dto)
        //{
        //    var result = this._service.ObterUsuario(id);

        //    if (result == null)
        //        return NotFound();

        //    _service.UpdateUsuario(id, dto);
        //    return Ok(result);
        //}

    }
}
