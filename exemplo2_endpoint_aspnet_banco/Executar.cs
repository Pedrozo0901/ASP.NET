using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Chamar as bibliotecas do ASP.NET
// Executar o comando para instalar o pacote:
// dotnet add package Microsoft.AspNetCore
// dotnet add package Swashbuckle.AspNetCore
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using exemplo2_endpoint_aspnet_banco.database;


namespace exemplo2_endpoint_aspnet_banco
{
    public class Executar
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurar a string de conexão com o banco de dados
            var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

            // Registrar o AppDbContext com postgres
            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Chamando o Swaggr
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}