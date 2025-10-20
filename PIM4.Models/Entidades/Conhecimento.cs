using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM4.Models.Entidades
{
    [Table("Conhecimento")]
    public class Conhecimento
    {
        [Key]
        public int IdConhecimento { get; set; }

        [Required]
        public string Categoria { get; set; } = string.Empty;

        [Required]
        public string TituloArtigo { get; set; } = string.Empty;
        
        [Required]
        public string ConteudoResumido { get; set; } = string.Empty;

        public string? LinkCompleto { get; set; }
    }
}