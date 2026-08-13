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
                    return response; 
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

        public Task<ResponseModel<UsuarioModel>> ObtenerUsuarioPorId(int id)
        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {
                var usuario = _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id).Result;
                if (usuario == null)
                {
                    response.Mensagem = "Usuário não encontrado.";
                    return Task.FromResult(response);
                }

                response.Dados = usuario;
                response.Mensagem = "Usuário localizado com sucesso.";
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao obter usuário: {ex.Message}";
                response.Status = false;
                return Task.FromResult(response);
            }
        }
    }
}
