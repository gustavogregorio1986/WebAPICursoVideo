using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
