using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM4.Models.Entidades
{
    [Table("Log_Acesso")]
    public class LogAcesso
    {
        [Key]
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        [Required]
        [StringLength(50)]
        public string Acao { get; set; } = string.Empty;

        public DateTime DataAcesso { get; set; }
    }
}