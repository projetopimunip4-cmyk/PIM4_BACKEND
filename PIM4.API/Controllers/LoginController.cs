using Microsoft.AspNetCore.Mvc;
using PIM4.Services;
using System.Threading.Tasks;
using System;

namespace PIM4.API.Controllers
{
    // DTO (Data Transfer Object) para receber as credenciais do Postman
    public class CredenciaisDTO 
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }

    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public LoginController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Endpoint para autenticação do usuário.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] CredenciaisDTO credenciais)
        {
            // 1. Validação básica (o ASP.NET Core faz a validação do tipo)
            if (string.IsNullOrWhiteSpace(credenciais.Email) || string.IsNullOrWhiteSpace(credenciais.Senha))
            {
                return BadRequest(new { message = "E-mail e Senha são obrigatórios." });
            }

            // 2. Autenticar o usuário
            var usuario = await _usuarioService.Autenticar(credenciais.Email, credenciais.Senha);

            if (usuario == null)
            {
                // Falha no login (401 Unauthorized)
                return Unauthorized(new { message = "Credenciais inválidas. Verifique e-mail e senha." }); 
            }
            
            // 3. Sucesso no Login (Retorna 200 OK)
            return Ok(new 
            { 
                message = "Login efetuado com sucesso.",
                idUsuario = usuario.IdUsuario,
                nome = usuario.Nome,
                tipoUsuario = usuario.TipoUsuario
            });
        }
    }
}