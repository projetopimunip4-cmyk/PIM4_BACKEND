using Microsoft.AspNetCore.Mvc;
using PIM4.Services;
using PIM4.Models.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using PIM4.Data.Repositorios;

namespace PIM4.API.Controllers
{
    [ApiController]
    // ROTA EXPLÍCITA: Força o uso de /api/Usuarios, resolvendo o 404.
    [Route("api/Usuarios")] 
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly UsuarioRepositorio _usuarioRepositorio;

        // O construtor injeta as dependências necessárias
        public UsuariosController(UsuarioService usuarioService, UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioService = usuarioService;
            _usuarioRepositorio = usuarioRepositorio;
        }

        // Endpoint POST para registrar um novo usuário (Rota: POST /api/Usuarios)
        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                // Validação de senha simples: o serviço deve ser chamado com a senha em texto puro,
                // que, no seu modelo, está no campo SenhaHash (usado como workaround).
                if (string.IsNullOrWhiteSpace(usuario.SenhaHash))
                {
                    return BadRequest(new { message = "O campo de senha é obrigatório e deve conter a senha em texto puro." });
                }

                // Chama o serviço para criar o usuário e hashear a senha.
                var novoUsuario = await _usuarioService.CriarUsuario(usuario, usuario.SenhaHash); 
                
                // Retorna 201 Created com a URL para o novo recurso
                return CreatedAtAction(nameof(BuscarUsuarioPorId), new { id = novoUsuario.IdUsuario }, novoUsuario);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno. Verifique o servidor e o banco de dados.");
            }
        }
        
        // Método GET necessário para o CreatedAtAction funcionar (Rota: GET /api/Usuarios/{id})
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> BuscarUsuarioPorId(int id)
        {
            var usuario = await _usuarioRepositorio.BuscarPorId(id); 
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }
        
        // Exemplo de listagem (opcional)
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> ListarTodos()
        {
            var usuarios = await _usuarioRepositorio.ListarTodos();
            return Ok(usuarios);
        }
    }
}