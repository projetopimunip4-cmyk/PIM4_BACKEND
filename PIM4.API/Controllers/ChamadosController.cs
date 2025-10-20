using Microsoft.AspNetCore.Mvc;
using PIM4.Models.Entidades;
using PIM4.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace PIM4.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Rota base: /api/Chamados
    public class ChamadosController : ControllerBase
    {
        private readonly ChamadoService _chamadoService;

        public ChamadosController(ChamadoService chamadoService)
        {
            _chamadoService = chamadoService;
        }

        // --- ENDPOINTS DE ACOMPANHAMENTO (GET E PUT) ---

        // 1. GET por ID (Mais específico)
        /// <summary>
        /// Método GET para buscar chamado por ID (Rota: /api/Chamados/{id}).
        /// </summary>
        [HttpGet("{id}")] 
        public async Task<ActionResult<Chamado>> BuscarChamadoPorId(int id)
        {
            var chamado = await _chamadoService.BuscarPorId(id);
            if (chamado == null) return NotFound(); 
            return Ok(chamado);
        }

        // 2. PUT para Atualização de Status
        /// <summary>
        /// Método PUT para atualizar o status e detalhes de um chamado. (REQUISITO: P4)
        /// Rota: PUT /api/Chamados/{id}
        /// </summary>
        [HttpPut("{id}")] 
        public async Task<IActionResult> AtualizarChamado(int id, [FromBody] Chamado chamado)
        {
            if (id != chamado.IdChamado)
            {
                return BadRequest(new { message = "O ID da URL não corresponde ao ID do chamado fornecido." });
            }

            try
            {
                var sucesso = await _chamadoService.AtualizarStatus(chamado); 

                if (!sucesso)
                {
                    return NotFound(); // 404 se o chamado não existir
                }

                return NoContent(); // 204 No Content para sucesso
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao atualizar chamado."); 
            }
        }
        
        // 3. GET All (Mais genérico)
        /// <summary>
        /// Método GET para listar todos os chamados (Rota: /api/Chamados).
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Chamado>>> ListarChamados()
        {
            var chamados = await _chamadoService.ListarTodos();
            return Ok(chamados);
        }

        // --- ENDPOINT DE CRIAÇÃO (POST) ---
        
        /// <summary>
        /// Método POST para criar um novo chamado.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CriarChamado([FromBody] Chamado chamado)
        {
            try
            {
                var novoChamado = await _chamadoService.Criar(chamado);
                return CreatedAtAction(nameof(BuscarChamadoPorId), new { id = novoChamado.IdChamado }, novoChamado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message }); 
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao registrar chamado. Tente novamente.");
            }
        }
    }
}