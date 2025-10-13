using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM4.Models.Entidades
{
    [Table("IA_Sugestao")]
    public class IASugestao
    {
        [Key]
        [Column("id_sugestao")]
        public int IdSugestao { get; set; }

        [Required]
        [Column("id_chamado")]
        public int IdChamado { get; set; }

        [Column("categoria_sugerida", TypeName = "VARCHAR(50)")]
        public string CategoriaSugerida { get; set; }

        [Column("solucao_sugerida", TypeName = "TEXT")]
        public string SolucaoSugerida { get; set; }

        [Column("confiabilidade", TypeName = "DECIMAL(5,2)")]
        public decimal Confiabilidade { get; set; }

        // Propriedade de navegação
        [ForeignKey("IdChamado")]
        public Chamado Chamado { get; set; }
    }
}