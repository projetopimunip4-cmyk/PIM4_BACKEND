using Microsoft.EntityFrameworkCore;
using PIM4.Data.Context;
using PIM4.Models.Entidades;
using PIM4.Models.DTOs; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIM4.Data.Repositorios
{
    public class ChamadoRepositorio
    {
        private readonly AppDbContext _contexto;

        public ChamadoRepositorio(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        // --- MÉTODOS DE CONSULTA (READ) OTIMIZADOS ---
        
        /// <summary>
        /// Busca um chamado pelo ID, incluindo o objeto do Usuário (Otimização com .Include()).
        /// </summary>
        public async Task<Chamado?> BuscarPorId(int id)
        {
            // OTIMIZAÇÃO APLICADA AQUI: Inclui o Usuário para evitar Lazy Loading (Múltiplas Queries)
            return await _contexto.Chamados
                                     .Include(c => c.Usuario) 
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(c => c.IdChamado == id);
        }

        /// <summary>
        /// Lista todos os chamados, incluindo o objeto do Usuário (Otimização com .Include()).
        /// </summary>
        public async Task<List<Chamado>> ListarTodos()
        {
            // OTIMIZAÇÃO APLICADA AQUI: Inclui o Usuário para carregar o nome de quem abriu o chamado
            return await _contexto.Chamados
                                     .Include(c => c.Usuario) 
                                     .AsNoTracking()
                                     .ToListAsync();
        }

        // --- MÉTODOS DE MANIPULAÇÃO (CREATE / UPDATE) ---
        public async Task<Chamado> Adicionar(Chamado chamado)
        {
            _contexto.Chamados.Add(chamado);
            await _contexto.SaveChangesAsync();
            return chamado;
        }

        public async Task<List<Chamado>> ListarChamadosPaginados(int page, int pageSize)
        {
            return await _contexto.Chamados
                .Include(c => c.Usuario)
                .OrderByDescending(c => c.DataAbertura)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
        
        /// <summary>
        /// Atualiza o status e detalhes de um chamado existente (REQUISITO: P4).
        /// </summary>
        public async Task<bool> Atualizar(Chamado chamado)
        {
            var chamadoExistente = await _contexto.Chamados.FindAsync(chamado.IdChamado);

            if (chamadoExistente == null)
            {
                return false; 
            }

            chamadoExistente.Status = chamado.Status;
            chamadoExistente.Prioridade = chamado.Prioridade; 
            chamadoExistente.Categoria = chamado.Categoria;

            await _contexto.SaveChangesAsync();
            return true;
        }

        // --- MÉTODOS DE RELATÓRIOS (DASHBOARD) ---
        public async Task<EstatisticasDTO> ObterEstatisticasTotais()
        {
            var dados = await _contexto.Chamados.AsNoTracking().ToListAsync(); 

            return new EstatisticasDTO
            {
                ChamadosAbertos = dados.Count(c => c.Status == "aberto"),
                ChamadosEmAtendimento = dados.Count(c => c.Status == "em atendimento"),
                ChamadosResolvidos = dados.Count(c => c.Status == "resolvido"),
                TotalGeral = dados.Count()
            };
        }

        public async Task<List<ChamadosPorStatusDTO>> ObterChamadosPorStatus()
        {
            return await _contexto.Chamados
                .GroupBy(c => c.Status) 
                .Select(g => new ChamadosPorStatusDTO
                {
                    Status = g.Key, 
                    Quantidade = g.Count() 
                })
                .OrderBy(d => d.Status) 
                .ToListAsync();
        }
    }
}