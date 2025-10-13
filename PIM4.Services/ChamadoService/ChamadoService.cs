using PIM4.Data.Repositorios;
using PIM4.Models.Entidades;
using System;
using System.Collections.Generic; // Adicionado para List<Chamado>
using System.Threading.Tasks;

namespace PIM4.Services
{
    public class ChamadoService
    {
        private readonly ChamadoRepositorio _chamadoRepositorio;

        public ChamadoService(ChamadoRepositorio chamadoRepositorio)
        {
            _chamadoRepositorio = chamadoRepositorio;
        }

        // --- MÉTODOS DE CONSULTA (LEITURA/ACOMPANHAMENTO) ---

        public async Task<List<Chamado>> ListarTodos()
        {
            return await _chamadoRepositorio.ListarTodos();
        }
        
        // CORREÇÃO: Método de Serviço que atua como ponte para o Repositório
        public async Task<Chamado?> BuscarPorId(int id)
        {
            return await _chamadoRepositorio.BuscarPorId(id);
        }

        // --- LÓGICA DE NEGÓCIO: CRIAÇÃO ---

        public async Task<Chamado> Criar(Chamado chamado)
        {
            // Validação (Regra: Registro)
            if (string.IsNullOrWhiteSpace(chamado.Titulo) || string.IsNullOrWhiteSpace(chamado.Descricao))
            {
                throw new ArgumentException("Título e Descrição do chamado são obrigatórios.");
            }

            // 1. Simulação da IA para Priorização e Encaminhamento (Regras: Priorização, Encaminhamento)
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

            // 2. Definição do Status e Data (Regras: Registro, Acompanhamento)
            chamado.DataAbertura = DateTime.Now;
            chamado.Status = "aberto";

            // 3. Persistência
            return await _chamadoRepositorio.Adicionar(chamado);
        }
    }
}