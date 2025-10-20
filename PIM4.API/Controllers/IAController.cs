using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PIM4.Data.Repositorios; // Necessário para injetar o Repositório
using PIM4.Models.DTOs; // Necessário para os DTOs de retorno
using System;

namespace PIM4.API.Controllers
{
    // DTO para receber a descrição do chamado para a IA analisar
    public class SugestaoRequestDTO 
    {
        public string DescricaoChamado { get; set; } = string.Empty;
    }

    [ApiController]
    [Route("api/ia")] 
    public class IAController : ControllerBase
    {
        private readonly ConhecimentoRepositorio _conhecimentoRepositorio;

        public IAController(ConhecimentoRepositorio conhecimentoRepositorio)
        {
            _conhecimentoRepositorio = conhecimentoRepositorio;
        }

        /// <summary>
        /// POST /api/ia/base-conhecimento: Simula a triagem da IA e retorna artigos da Base de Conhecimento.
        /// (Requisito: Soluções Automáticas, IA - ChatGPT)
        /// </summary>
        [HttpPost("base-conhecimento")]
        public async Task<ActionResult<BaseConhecimentoDTO>> SugerirBaseConhecimento([FromBody] SugestaoRequestDTO request)
        {
            // 1. Simulação da Categoria (Lógica que a IA faria)
            string categoriaSugerida;
            
            if (request.DescricaoChamado.ToLower().Contains("servidor") || request.DescricaoChamado.ToLower().Contains("rede"))
            {
                categoriaSugerida = "Infraestrutura/Rede";
            }
            else if (request.DescricaoChamado.ToLower().Contains("acesso") || request.DescricaoChamado.ToLower().Contains("senha"))
            {
                categoriaSugerida = "Acesso/Usuário";
            }
            else
            {
                categoriaSugerida = "Geral/Outros";
            }
            
            // 2. Buscar Artigos Relevantes na Base de Dados (Simulando a consulta do Gemini/IA)
            var artigos = await _conhecimentoRepositorio.BuscarArtigosPorCategoria(categoriaSugerida);

            // 3. Retornar a Estrutura da Base de Conhecimento (DTO)
            return Ok(new BaseConhecimentoDTO
            {
                CategoriaReconhecida = categoriaSugerida,
                DescricaoIA = $"Com base na sua descrição, encontramos {artigos.Count} artigos relevantes em nossa Base de Conhecimento:",
                ArtigosRelacionados = artigos
            });
        }
    }
}