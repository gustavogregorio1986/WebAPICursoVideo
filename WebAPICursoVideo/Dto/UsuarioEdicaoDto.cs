using System.ComponentModel.DataAnnotations;

namespace WebAPICursoVideo.Dto
{
    public class UsuarioEdicaoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o Usuario")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Digite o Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o Sobrenome")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Digite o Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o Data da Criação")]
        public DateTime DataCriacao { get; set; }

        [Required(ErrorMessage = "Digite o Data da Edição")]
        public DateTime DataAlteracao { get; set; }

        [Required(ErrorMessage = "Digite a Senha")]
        public string SenhaHash { get; set; }

        [Required(ErrorMessage = "Digite a confirmação da Senha"),
            Compare("SenhaHash", ErrorMessage = "As senhas não coincidem.")]
        public string SenhaSalt { get; set; }
    }
}
