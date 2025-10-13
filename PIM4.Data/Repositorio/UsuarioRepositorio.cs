using Microsoft.EntityFrameworkCore;
using PIM4.Data.Context;
using PIM4.Models.Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIM4.Data.Repositorios
{
    // A classe principal que implementa as operações de acesso a dados.
    public class UsuarioRepositorio
    {
        private readonly AppDbContext _contexto;

        // Construtor: Recebe o contexto do banco de dados via Injeção de Dependência.
        public UsuarioRepositorio(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        // --- MÉTODOS DE CONSULTA (READ) ---

        /// <summary>
        /// Lista todos os usuários cadastrados.
        /// </summary>
        public async Task<List<Usuario>> ListarTodos()
        {
            return await _contexto.Usuarios.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Busca um usuário pelo ID.
        /// </summary>
        public async Task<Usuario?> BuscarPorId(int id)
        {
            // Usa FindAsync para buscar por chave primária.
            return await _contexto.Usuarios.FindAsync(id); 
        }
        
        /// <summary>
        /// Busca um usuário pelo e-mail (essencial para o Login).
        /// </summary>
        public async Task<Usuario?> BuscarPorEmail(string email)
        {
            return await _contexto.Usuarios
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(u => u.Email == email);
        }

        // --- MÉTODOS DE MANIPULAÇÃO (CREATE/UPDATE/DELETE) ---

        /// <summary>
        /// Adiciona um novo usuário ao banco de dados.
        /// </summary>
        public async Task<Usuario> Adicionar(Usuario usuario)
        {
            _contexto.Usuarios.Add(usuario);
            await _contexto.SaveChangesAsync();
            return usuario;
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        public async Task<Usuario?> Atualizar(Usuario usuario)
        {
            var usuarioExistente = await _contexto.Usuarios.FindAsync(usuario.IdUsuario);

            if (usuarioExistente == null)
                return null;

            // Mapeamento de propriedades para rastreamento
            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.Telefone = usuario.Telefone;
            usuarioExistente.TipoUsuario = usuario.TipoUsuario;
            usuarioExistente.SenhaHash = usuario.SenhaHash;
            
            await _contexto.SaveChangesAsync();
            return usuarioExistente;
        }

        /// <summary>
        /// Remove um usuário existente do banco de dados.
        /// </summary>
        public async Task Remover(Usuario usuario)
        {
            var usuarioARemover = await _contexto.Usuarios.FindAsync(usuario.IdUsuario);

            if (usuarioARemover != null)
            {
                _contexto.Usuarios.Remove(usuarioARemover);
                await _contexto.SaveChangesAsync();
            }
        }
    }
}