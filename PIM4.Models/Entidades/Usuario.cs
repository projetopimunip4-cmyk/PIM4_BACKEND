using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM4.Models.Entidades
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int IdUsuario { get; set; } 

        [StringLength(100)]
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;


        [EmailAddress]
        [StringLength(100)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [StringLength(15)]
        [Column("telefone")]
        public string? Telefone { get; set; }


        [StringLength(20)]
        [Column("tipo_usuario")]
        public string TipoUsuario { get; set; } = string.Empty;


        [StringLength(255)]
        [Column("senha_hash")]
        public string SenhaHash { get; set; } = string.Empty;

        [NotMapped]
        public string Senha { get; set; } = string.Empty;
    }
}