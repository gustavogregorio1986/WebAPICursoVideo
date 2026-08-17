using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPICursoVideo.Models;

namespace WebAPICursoVideo.Services.Senha
{
    public class SenhaService : ISenhaInterface
    {
        private readonly IConfiguration _config;

        public SenhaService(IConfiguration config)
        {
            _config = config;
        }

        public void CriarSenhaHash(string senha, out byte[] senhahas, out byte[] senhaSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhahas = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }

        public string CriarToken(UsuarioModel usuario)
        {
            List<Claim> claims = new List<Claim>
    {
        new Claim("Email", usuario.Nome),
        new Claim("Username", usuario.Usuario),
        new Claim("Id", usuario.Id.ToString())
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool VerificarSenhaHash(string senha, byte[] senhaHash, byte[] senhaSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(senhaSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return computedHash.SequenceEqual(senhaHash);
            }
        }
    }
}
