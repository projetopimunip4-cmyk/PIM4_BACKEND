using Microsoft.EntityFrameworkCore;
using PIM4.Data.Context;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using PIM4.Data.Repositorios;
using PIM4.Services;
using PIM4.API.Controllers;
using System; 

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração do DbContext e MySQL (Lendo do appsettings.json)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString,
        // Alinhado com a versão 9.0.0 dos pacotes Pomelo e EF Core
        new MySqlServerVersion(new Version(8, 0, 21)))); 

// 2. Injeção de Dependência: Adicionando Repositórios e Serviços (Lógica de Negócio)
builder.Services.AddScoped<UsuarioRepositorio>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ChamadoRepositorio>(); 
builder.Services.AddScoped<ChamadoService>();
// NOVO: Registro do Repositório de Conhecimento para a funcionalidade de IA
builder.Services.AddScoped<ConhecimentoRepositorio>(); 

// 3. Configurações da API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

var app = builder.Build();

// Pipeline de Requisições
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();