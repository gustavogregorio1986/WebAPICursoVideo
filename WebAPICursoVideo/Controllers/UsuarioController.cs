using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPICursoVideo.Dto;
using WebAPICursoVideo.Models;
using WebAPICursoVideo.Services.Usuario;

namespace WebAPICursoVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpGet]
        public async Task<ActionResult> ListarUsuarios()
        {
            var usuario = await _usuarioInterface.ListarUsuarios();
            return Ok(usuario);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> ObterUsuarioPorId(int id)
        {
            var usuario = await _usuarioInterface.ObterUsuarioPorId(id);
            return Ok(usuario);
        }

        [HttpPut]
        public async Task<ActionResult> EditarUsuario(UsuarioEdicaoDto usuarioEdicaoDto)
        {
            var usuario = await _usuarioInterface.EditarUsuario(usuarioEdicaoDto);
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _usuarioInterface.RemoverUsuario(id);
            return Ok(usuario);
        }


    }
}
