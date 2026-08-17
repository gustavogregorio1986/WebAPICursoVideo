using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPICursoVideo.Dto;
using WebAPICursoVideo.Models;
using WebAPICursoVideo.Services.Senha;
using WebAPICursoVideo.Services.Usuario;

namespace WebAPICursoVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;
        private readonly ISenhaInterface _senhaInterface;

        public LoginController(IUsuarioInterface usuarioInterface, ISenhaInterface senhaInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegistrarUsuario(UsuarioCriacaoDto usuarioCriacaoDto1)
        {
            var response = await _usuarioInterface.RegistrarUsuario(usuarioCriacaoDto1);
            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioModel usuario)
        {
            var token = _senhaInterface.CriarToken(usuario);
            return Ok(new { token });
        }
    }
}
