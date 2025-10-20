using Microsoft.EntityFrameworkCore;
using PIM4.Data.Context;
using PIM4.Models.Entidades;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace PIM4.Data.Repositorios
{
    public class ConhecimentoRepositorio
    {
        private readonly AppDbContext _contexto;

        public ConhecimentoRepositorio(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        /// <summary>
        /// Busca artigos relevantes com base na categoria sugerida pela IA.
        /// </summary>
        public async Task<List<Conhecimento>> BuscarArtigosPorCategoria(string categoria)
        {
            // Simula a busca de artigos de suporte por categoria
            // Usa Contains para simular a relevância baseada na categoria (como 'Infraestrutura/Rede')
            return await _contexto.Conhecimentos
                .AsNoTracking()
                .Where(c => c.Categoria.Contains(categoria))
                .ToListAsync();
        }

        /// <summary>
        /// Adiciona um novo artigo de Conhecimento (para testes ou seed de dados).
        /// </summary>
        public async Task<Conhecimento> Adicionar(Conhecimento artigo)
        {
            _contexto.Conhecimentos.Add(artigo);
            await _contexto.SaveChangesAsync();
            return artigo;
        }
    }
}