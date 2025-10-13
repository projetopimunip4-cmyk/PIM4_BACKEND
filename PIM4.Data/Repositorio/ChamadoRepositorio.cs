using Microsoft.EntityFrameworkCore;
using PIM4.Data.Context;
using PIM4.Models.Entidades;
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

        // --- MÉTODOS DE CONSULTA (READ) ---
        public async Task<Chamado?> BuscarPorId(int id)
        {
            // CORREÇÃO: Certifique-se de usar 'IdChamado' com 'I' e 'C' maiúsculos.
            return await _contexto.Chamados
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(c => c.IdChamado == id);
        }

        public async Task<List<Chamado>> ListarTodos()
{
    return await _contexto.Chamados.AsNoTracking().ToListAsync();
}

        // --- MÉTODOS DE MANIPULAÇÃO (CREATE) ---
        public async Task<Chamado> Adicionar(Chamado chamado)
        {
            _contexto.Chamados.Add(chamado);
            await _contexto.SaveChangesAsync();
            return chamado;
        }


        // ... (outros métodos CRUD)
    }
}