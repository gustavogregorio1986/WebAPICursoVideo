using Microsoft.EntityFrameworkCore;
using WebAPICursoVideo.Data;
using WebAPICursoVideo.Models;

namespace WebAPICursoVideo.Services.Usuario
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<UsuarioModel>>> ListarUsuarios()
        {
            ResponseModel<List<UsuarioModel>> response = new ResponseModel<List<UsuarioModel>>();

            try
            {
                var usuarios = await _context.Usuarios.ToListAsync();
                if(usuarios.Count() == 0)
                {
                    response.Mensagem = "Nenhum usuario cadastrado.";
                    return response;ele 
                }
                response.Dados = usuarios;
                response.Mensagem = "Usuario Localizado com sucesso.";

                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao listar usuários: {ex.Message}";
                response.Status = false;
                return response;
            }
        }
    }
}
