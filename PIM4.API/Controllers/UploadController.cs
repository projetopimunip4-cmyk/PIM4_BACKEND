// Dentro de PIM4.API/Controllers/UploadController.cs

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace PIM4.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Rota: /api/Upload
    public class UploadController : ControllerBase
    {
        /// <summary>
        /// POST /api/Upload: Endpoint para receber arquivos e simular upload para storage (REQUISITO: Mobile P5).
        /// </summary>
        [HttpPost]
        public IActionResult UploadArquivo([FromForm] IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                // Tratamento de exceção de dados (melhor prática)
                return BadRequest(new { message = "Nenhum arquivo enviado ou o arquivo está vazio." });
            }

            // Simulação de processamento e upload
            var nomeArquivo = Path.GetFileName(arquivo.FileName);
            var urlTemporaria = $"https://simulacao.azure.com/{nomeArquivo}";

            // Como o upload é síncrono (simulado), não precisamos do 'async' ou 'await'
            return Ok(new 
            { 
                message = "Upload simulado com sucesso. Arquivo pronto para anexar ao chamado.",
                nome = nomeArquivo,
                url = urlTemporaria
            });
        }
    }
}