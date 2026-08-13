using WebAPICursoVideo.Models;

namespace WebAPICursoVideo.Services.Usuario
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<UsuarioModel>>> ListarUsuarios();

        Task<ResponseModel<UsuarioModel>> ObtenerUsuarioPorId(int id);
    }
}
