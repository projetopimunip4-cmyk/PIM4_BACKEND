using Microsoft.EntityFrameworkCore;
using PIM4.Models.Entidades; // Certifique-se de que este using está correto

namespace PIM4.Data.Context
{
    public class AppDbContext : DbContext
    {
        // Construtor que recebe as opções de conexão
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        

        // Mapeamento das Entidades para as Tabelas do Banco de Dados (DbSets)
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Resposta> Respostas { get; set; }
        public DbSet<IASugestao> IASugestoes { get; set; }
        public DbSet<Anexo> Anexos { get; set; }
        public DbSet<LogAcesso> LogAcessos { get; set; }
        
        // NOVO: Adicionando o DbSet para a Base de Conhecimento
        public DbSet<Conhecimento> Conhecimentos { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeamento explícito (Opcional, mas ajuda a manter o padrão de nomes)
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Chamado>().ToTable("Chamado");
            modelBuilder.Entity<Resposta>().ToTable("Resposta");
            modelBuilder.Entity<IASugestao>().ToTable("IA_Sugestao");
            modelBuilder.Entity<Anexo>().ToTable("Anexo");
            modelBuilder.Entity<LogAcesso>().ToTable("Log_Acesso");
            
            // NOVO: Mapeamento da tabela Conhecimento
            modelBuilder.Entity<Conhecimento>().ToTable("Conhecimento");

            // Configurações de chaves estrangeiras, se necessário (exemplo)
            // modelBuilder.Entity<Chamado>()
            //    .HasOne(c => c.Usuario)
            //    .WithMany(u => u.Chamados)
            //    .HasForeignKey(c => c.IdUsuario); 
        }
    }
}