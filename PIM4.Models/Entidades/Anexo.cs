using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM4.Models.Entidades
{
    [Table("Anexo")]
    public class Anexo
    {
        [Key]
        [Column("id_anexo")]
        public int IdAnexo { get; set; }

        [Required]
        [Column("id_chamado")]
        public int IdChamado { get; set; }

        [Column("nome_arquivo", TypeName = "VARCHAR(255)")]
        public string? NomeArquivo { get; set; } // Ajustado para ser anulável

        [Column("caminho_arquivo", TypeName = "VARCHAR(255)")]
        public string? CaminhoArquivo { get; set; } // Ajustado para ser anulável

        [Column("data_upload")]
        public DateTime DataUpload { get; set; } = DateTime.Now;

        // Propriedade de navegação para a chave estrangeira
        [ForeignKey("IdChamado")]
        public Chamado? Chamado { get; set; } // Ajustado para ser anulável
    }
}