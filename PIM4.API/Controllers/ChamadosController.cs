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

        // --- ENDPOINTS DE ACOMPANHAMENTO (GET) ---

        // 1. MÉTODO MAIS ESPECÍFICO: GET por ID
        /// <summary>
        /// Método GET para buscar chamado por ID (Rota: /api/Chamados/{id}).
        /// O atributo "{id}" torna esta rota a mais específica, devendo vir primeiro.
        /// </summary>
        [HttpGet("{id}")] 
        public async Task<ActionResult<Chamado>> BuscarChamadoPorId(int id)
        {
            var chamado = await _chamadoService.BuscarPorId(id); 

            if (chamado == null)
            {
                return NotFound(); // 404 se não encontrar o chamado
            }
            return Ok(chamado); // 200 OK com os detalhes
        }

        // 2. MÉTODO MAIS GENÉRICO: GET All
        /// <summary>
        /// Método GET para listar todos os chamados (Rota: /api/Chamados).
        /// Atende ao requisito de Acompanhamento (Lista de Chamados).
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Chamado>>> ListarChamados()
        {
            var chamados = await _chamadoService.ListarTodos();
            return Ok(chamados);
        }

        // --- ENDPOINT DE CRIAÇÃO (POST) ---

        /// <summary>
        /// Método POST para criar um novo chamado (Regra de Registro/Priorização).
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CriarChamado([FromBody] Chamado chamado)
        {
            try
            {
                var novoChamado = await _chamadoService.Criar(chamado);

                // Retorna 201 Created e o caminho para o novo recurso
                return CreatedAtAction(nameof(BuscarChamadoPorId), new { id = novoChamado.IdChamado }, novoChamado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message }); // 400 Bad Request
            }
            catch (Exception)
            {
                // Este é o retorno genérico para erros de banco de dados ou serviço
                return StatusCode(500, "Erro ao registrar chamado. Tente novamente."); 
            }
        }
    }
}