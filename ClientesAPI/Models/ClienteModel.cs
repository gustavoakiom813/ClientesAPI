using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientesAPI.Models
{
    [Table("Clientes")]
    public class ClienteModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [MaxLength(18, ErrorMessage = "O CPF deve ter no máximo 14 caracteres.")]
        public string CPF { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
