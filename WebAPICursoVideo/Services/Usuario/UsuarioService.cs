using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPICursoVideo.Data;
using WebAPICursoVideo.Dto;
using WebAPICursoVideo.Models;
using WebAPICursoVideo.Services.Senha;

namespace WebAPICursoVideo.Services.Usuario
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly AppDbContext _context;
        private readonly ISenhaInterface _senhaInterface;
        private readonly IMapper _mapper;

        public UsuarioService(AppDbContext context, ISenhaInterface senhaInterface, IMapper mapper)
        {
            _context = context;
            _senhaInterface = senhaInterface;
            _mapper = mapper;
        }

        public async Task<ResponseModel<UsuarioModel>> EditarUsuario(UsuarioEdicaoDto usuarioEdicaoDto)
        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {
                var usuarioBanco = await _context.Usuarios
                         .FirstOrDefaultAsync(u => u.Id == usuarioEdicaoDto.Id);


                if (usuarioBanco == null)
                {
                    response.Mensagem = "Usuário não encontrado.";
                    return response;
                }

                _mapper.Map(usuarioEdicaoDto, usuarioBanco);

                _context.Update(usuarioBanco);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.Mensagem = "Usuário editado com sucesso.";
                response.Dados = usuarioBanco;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
            }

            return response;
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

        public async Task<ResponseModel<UsuarioModel>> Login(UsuarioLoginDto usuarioLoginDto)
        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == usuarioLoginDto.Email);

                if (usuario == null)
                {
                    response.Mensagem = "Credenciais invalidas!";
                    response.Status = false;
                    return response;
                }

                if (!_senhaInterface.VerificarSenhaHash(usuarioLoginDto.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    response.Mensagem = "Email ou senha incorretos.";
                    response.Status = false;
                    return response;
                }

                response.Status = true;
                response.Mensagem = "Login realizado com sucesso.";
                response.Dados = usuario;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }

            return response;
        }

        public Task<ResponseModel<UsuarioModel>> ObterUsuarioPorId(int id)
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

        public async Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            var response = new ResponseModel<UsuarioModel>();

            try
            {
                if (!VeficaSeExisteEmailUsuarioRepetidos(usuarioCriacaoDto))
                {
                    response.Mensagem = "Email/usuário já cadastrado.";
                    response.Status = false;
                    return response;
                }

                _senhaInterface.CriarSenhaHash(usuarioCriacaoDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                UsuarioModel usuario = _mapper.Map<UsuarioModel>(usuarioCriacaoDto);
                usuario.SenhaHash = senhaHash;
                usuario.SenhaSalt = senhaSalt;
                usuario.DataCriacao = DateTime.Now;
                usuario.DataAlteracao = DateTime.Now;

                _context.Add(usuario);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.Mensagem = "Usuário cadastrado com sucesso.";
                response.Dados = usuario;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<UsuarioModel>> RemoverUsuario(int id)
        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

                if (usuario == null)
                {
                    response.Mensagem = "Usuário não encontrado.";
                    response.Status = false;
                    return response;
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                response.Mensagem = $"Usuário {usuario.Nome} removido com sucesso.";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao remover usuário: {ex.Message}";
                response.Status = false;
                return response;
            }
        }

        private  bool VeficaSeExisteEmailUsuarioRepetidos(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == usuarioCriacaoDto.Email || 
                               u.Email == usuarioCriacaoDto.Email);

            if(usuario != null)
            {
                return false;
            }

            return true;
        }
    }
}
