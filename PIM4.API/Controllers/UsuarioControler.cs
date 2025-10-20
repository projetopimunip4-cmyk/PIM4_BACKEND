using Microsoft.AspNetCore.Mvc;
using PIM4.Services;
using PIM4.Models.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace PIM4.API.Controllers
{
    [ApiController]
    [Route("api/Usuarios")] 
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService; // APENAS O SERVICE

        // O construtor injeta APENAS o Service (Boa Prática de Arquitetura Limpa)
        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // Endpoint POST para registrar um novo usuário
        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                // Chama o serviço para criar o usuário e hashear a senha.
                var novoUsuario = await _usuarioService.CriarUsuario(usuario, usuario.SenhaHash); 
                
                return CreatedAtAction(nameof(BuscarUsuarioPorId), new { id = novoUsuario.IdUsuario }, novoUsuario);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno.");
            }
        }
        
        // --- MÉTODOS DE CONSULTA (CHAMANDO O SERVICE) ---
        
        // Método GET por ID - Rota: /api/Usuarios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> BuscarUsuarioPorId(int id)
        {
            // CORREÇÃO: Chama o Service, que deve ter este método (CS1061 resolvido)
            var usuario = await _usuarioService.BuscarUsuarioPorId(id); 
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }
        
        // Método GET para listagem - Rota: /api/Usuarios
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> ListarTodos()
        {
            // CORREÇÃO: Chama o Service, que deve ter este método (CS1061 resolvido)
            var usuarios = await _usuarioService.ListarTodos();
            return Ok(usuarios);
        }
        
        // ... (Os métodos PUT e DELETE devem ser ajustados para chamar o _usuarioService)
    }
}