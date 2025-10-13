using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; // Necessário para a anotação [JsonIgnore]

namespace PIM4.Models.Entidades
{
    [Table("Chamado")]
    public class Chamado
    {
        [Key]
        [Column("id_chamado")] 
        public int IdChamado { get; set; } 

        // Chave Estrangeira explícita (o que você envia no JSON)
        [Required]
        [Column("id_usuario")] 
        public int IdUsuario { get; set; } 

        [Required]
        [StringLength(100)]
        [Column("titulo")]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [Column("descricao", TypeName = "TEXT")]
        public string Descricao { get; set; } = string.Empty;

        [Column("data_abertura")]
        public DateTime DataAbertura { get; set; } = DateTime.Now;

        [Required]
        [StringLength(20)]
        [Column("status")]
        public string Status { get; set; } = "aberto";

        [Required]
        [StringLength(10)]
        [Column("prioridade")]
        public string Prioridade { get; set; } = "baixa";

        [StringLength(50)]
        [Column("categoria")]
        public string? Categoria { get; set; }

        // Propriedade de navegação: IGNORADA na entrada do JSON
        // CRUCIAL: Impede que o ASP.NET Core exija o objeto Usuario na requisição POST
        [JsonIgnore] 
        [ForeignKey("IdUsuario")] 
        public Usuario? Usuario { get; set; } // Adicionado o '?' para nulabilidade
    }
}