using System; 
using System.Threading.Tasks;
using PIM4.Models.Entidades;
using PIM4.Data.Repositorios; 
using BCrypt.Net;
using System.Collections.Generic; // Necessário para List<Usuario>

namespace PIM4.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioService(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        // --- MÉTODOS PONTE PARA O REPOSITÓRIO (Resolvendo CS1061) ---

        /// <summary>
        /// Ponte para o repositório para buscar um usuário pelo ID.
        /// </summary>
        public async Task<Usuario?> BuscarUsuarioPorId(int id)
        {
            return await _usuarioRepositorio.BuscarPorId(id); 
        }

        /// <summary>
        /// Ponte para o repositório para listar todos os usuários.
        /// </summary>
        public async Task<List<Usuario>> ListarTodos()
        {
            return await _usuarioRepositorio.ListarTodos();
        }

        /// <summary>
        /// Ponte para o repositório para listar todos os técnicos.
        /// </summary>
        public async Task<List<Usuario>> ListarTecnicos()
        {
            return await _usuarioRepositorio.BuscarPorTipo("tecnico"); 
        }
        
        // --- MÉTODOS DE TRANSAÇÃO (Registro e Autenticação) ---
        
        /// <summary>
        /// Cria um novo usuário, aplicando o hash na senha antes de salvar.
        /// </summary>
        public async Task<Usuario> CriarUsuario(Usuario novoUsuario, string senha)
        {
            // Aplica o hash antes de salvar
            novoUsuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha);
            return await _usuarioRepositorio.Adicionar(novoUsuario);
        }

        /// <summary>
        /// Valida as credenciais do usuário para login.
        /// </summary>
        public async Task<Usuario?> Autenticar(string email, string senha)
        {
            var usuario = await _usuarioRepositorio.BuscarPorEmail(email);
            if (usuario == null) return null;

            if (BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
            {
                return usuario;
            }
            else
            {
                return null;
            }
        }
    }
}