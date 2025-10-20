using PIM4.Data.Repositorios;
using PIM4.Models.Entidades;
using PIM4.Models.DTOs; // Inclui os DTOs de Dashboard
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PIM4.Services
{
    public class ChamadoService
    {
        private readonly ChamadoRepositorio _chamadoRepositorio;

        public ChamadoService(ChamadoRepositorio chamadoRepositorio)
        {
            _chamadoRepositorio = chamadoRepositorio;
        }

        // -------------------------------------------------------------
        // --- MÉTODOS DE CONSULTA E RELATÓRIO (GET / Dashboard) ---
        // -------------------------------------------------------------
        
        // CORREÇÃO CS1061 (ChamadosController)
        public async Task<List<Chamado>> ListarTodos()
        {
            return await _chamadoRepositorio.ListarTodos();
        }
        
        // CORREÇÃO CS1061 (ChamadosController)
        public async Task<Chamado?> BuscarPorId(int id)
        {
            return await _chamadoRepositorio.BuscarPorId(id);
        }

        // CORREÇÃO CS1061 (DashboardController)
        public async Task<EstatisticasDTO> ObterEstatisticasTotais()
        {
            return await _chamadoRepositorio.ObterEstatisticasTotais();
        }
        public async Task<List<Chamado>> ListarChamadosPaginados(int page, int pageSize)
        {
            return await _chamadoRepositorio.ListarChamadosPaginados(page, pageSize);
        }

        // CORREÇÃO CS1061 (DashboardController)
        public async Task<List<ChamadosPorStatusDTO>> ObterChamadosPorStatus()
        {
            return await _chamadoRepositorio.ObterChamadosPorStatus();
        }

        // -------------------------------------------------------------
        // --- MÉTODOS DE TRANSAÇÃO (POST / PUT) ---
        // -------------------------------------------------------------
        
        // CORREÇÃO CS1061 (ChamadosController - Criar)
        public async Task<Chamado> Criar(Chamado chamado)
        {
            // Validação (Regra: Registro)
            if (string.IsNullOrWhiteSpace(chamado.Titulo) || string.IsNullOrWhiteSpace(chamado.Descricao))
            {
                throw new ArgumentException("Título e Descrição do chamado são obrigatórios.");
            }

            // Lógica de Priorização (IA)
            if (chamado.Descricao.ToLower().Contains("servidor") || chamado.Descricao.ToLower().Contains("rede"))
            {
                chamado.Categoria = "Infraestrutura/Rede";
                chamado.Prioridade = "alta";
            }
            else if (chamado.Descricao.ToLower().Contains("acesso") || chamado.Descricao.ToLower().Contains("senha"))
            {
                chamado.Categoria = "Acesso/Usuário";
                chamado.Prioridade = "media";
            }
            else
            {
                chamado.Categoria = "Geral/Outros";
                chamado.Prioridade = "baixa";
            }

            // Definição do Status e Data
            chamado.DataAbertura = DateTime.Now;
            chamado.Status = "aberto";

            return await _chamadoRepositorio.Adicionar(chamado);
        }
        
        // CORREÇÃO CS1061 (ChamadosController - Atualizar)
        public async Task<bool> AtualizarStatus(Chamado chamado)
        {
            return await _chamadoRepositorio.Atualizar(chamado);
        }
    }
}