namespace WebAPICursoVideo.Services.Senha
{
    public interface ISenhaInterface
    {
        void CriarSenhaHash(string senha, out byte[] senhahas, out byte[] senhaSalt);
    }
}
