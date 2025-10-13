using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM4.Models.Entidades
{
    [Table("Tecnico")]
    public class Tecnico
    {
        [Key]
        [Column("id_tecnico")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(255)]
        [Column("senha_hash")]
        public string SenhaHash { get; set; } = string.Empty;
    }
}