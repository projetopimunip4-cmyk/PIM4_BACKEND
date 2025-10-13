using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.IO;

namespace PIM4.Data.Context
{
    // Esta interface IDesignTimeDbContextFactory é crucial para o dotnet ef
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // 1. Configura o leitor de arquivos JSON (appsettings)
            var configuration = new ConfigurationBuilder()
                // Define o caminho base como o diretório do projeto PIM4.API
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "PIM4.API")) 
                .AddJsonFile("appsettings.json")
                .Build();

            // 2. Lê a string de conexão
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // 3. Cria o construtor de opções
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // 4. Configura o MySQL
            optionsBuilder.UseMySql(connectionString,
                new MySqlServerVersion(new Version(8, 0, 21)));

            // 5. Retorna a instância do contexto
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}