using System.ComponentModel.DataAnnotations;

namespace WebAPICursoVideo.Dto
{
    public class UsuarioCriacaoDto
    {
        [Required(ErrorMessage = "Digite o Usuario")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Digite o Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o Sobrenome")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Digite o Email")]
        public string Email { get; set; }

        public string Token { get; set; } = string.Empty;


        [Required(ErrorMessage = "Digite a Senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Digite a confirmação da Senha"),
            Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmarSenha { get; set; }
    }
}
