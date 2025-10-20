
using PIM4.Models.Entidades;
using System.Collections.Generic;
public class BaseConhecimentoDTO
{
    public string CategoriaReconhecida { get; set; } = string.Empty;
    public string DescricaoIA { get; set; } = "Aqui estão os artigos de nossa Base de Conhecimento que podem te ajudar.";
    public List<Conhecimento> ArtigosRelacionados { get; set; } = new List<Conhecimento>();
}