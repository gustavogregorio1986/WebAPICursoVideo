using WebAPICursoVideo.Dto;
using WebAPICursoVideo.Models;

namespace WebAPICursoVideo.Services.Usuario
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<UsuarioModel>>> ListarUsuarios();

        Task<ResponseModel<UsuarioModel>> ObterUsuarioPorId(int id);

        Task<ResponseModel<UsuarioModel>> RemoverUsuario(int id);

        Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioCriacaoDto usuarioCriacaoDto);
        Task<ResponseModel<UsuarioModel>> EditarUsuario(UsuarioEdicaoDto usuarioEdicaoDto);

        Task<ResponseModel<UsuarioModel>> Login(UsuarioLoginDto usuarioLoginDto);
    }
}
