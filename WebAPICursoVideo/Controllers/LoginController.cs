using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPICursoVideo.Dto;
using WebAPICursoVideo.Services.Usuario;

namespace WebAPICursoVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public LoginController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegistrarUsuario(UsuarioCriacaoDto usuarioCriacaoDto1)
        {
            var response = await _usuarioInterface.RegistrarUsuario(usuarioCriacaoDto1);
            return Ok(response);
        }
    }
}
