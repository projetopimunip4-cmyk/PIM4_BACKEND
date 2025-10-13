using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM4.Models.Entidades
{
    [Table("Resposta")]
    public class Resposta
    {
        [Key]
        [Column("id_resposta")]
        public int IdResposta { get; set; }

        [Required]
        [Column("id_chamado")]
        public int IdChamado { get; set; }

        [Required]
        [Column("id_tecnico")]
        public int IdTecnico { get; set; }

        [Column("data_resposta")]
        public DateTime DataResposta { get; set; } = DateTime.Now;

        [Required]
        [Column("texto_resposta", TypeName = "TEXT")]
        public string TextoResposta { get; set; }

        // Propriedades de navegação para as chaves estrangeiras
        [ForeignKey("IdChamado")]
        public Chamado Chamado { get; set; }

        [ForeignKey("IdTecnico")]
        public Usuario Tecnico { get; set; }
    }
}