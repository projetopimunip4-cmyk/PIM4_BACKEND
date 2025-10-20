using Microsoft.AspNetCore.Mvc;
using PIM4.Services;
using PIM4.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIM4.API.Controllers
{
    [ApiController]
    [Route("api/dashboard")] // Rota base específica para o Dashboard
    public class DashboardController : ControllerBase
    {
        private readonly ChamadoService _chamadoService;

        public DashboardController(ChamadoService chamadoService)
        {
            _chamadoService = chamadoService;
        }

        /// <summary>
        /// GET /api/dashboard/estatisticas: Retorna números totais de chamados.
        /// </summary>
        [HttpGet("estatisticas")]
        public async Task<ActionResult<EstatisticasDTO>> ObterEstatisticasTotais()
        {
            var estatisticas = await _chamadoService.ObterEstatisticasTotais();
            return Ok(estatisticas);
        }

        /// <summary>
        /// GET /api/dashboard/chamados-por-status: Retorna dados para gráficos.
        /// </summary>
        [HttpGet("chamados-por-status")]
        public async Task<ActionResult<List<ChamadosPorStatusDTO>>> ObterChamadosPorStatus()
        {
            var dados = await _chamadoService.ObterChamadosPorStatus();
            return Ok(dados);
        }
    }
}