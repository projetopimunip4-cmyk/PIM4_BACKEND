using Microsoft.AspNetCore.Mvc;
using PIM4.Services;
using PIM4.Models.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIM4.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Rota: /api/Tecnicos
    public class TecnicosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public TecnicosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// GET /api/Tecnicos: Retorna a lista de usuários com tipo_usuario = 'tecnico'.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> ListarTecnicos()
        {
            var tecnicos = await _usuarioService.ListarTecnicos();
            return Ok(tecnicos);
        }
    }
}