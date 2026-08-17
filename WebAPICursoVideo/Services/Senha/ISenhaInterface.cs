using WebAPICursoVideo.Models;

namespace WebAPICursoVideo.Services.Senha
{
    public interface ISenhaInterface
    {
        void CriarSenhaHash(string senha, out byte[] senhahas, out byte[] senhaSalt);

        bool VerificarSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt);

        string CriarToken(UsuarioModel usuario);
    }
}
