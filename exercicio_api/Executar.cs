using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Chamar as bibliotecas do ASP.NET
// Executar o comando para instalar o pacote: dotnet add package Microsoft.AspNetCore
// Comando para instalar o Swagger: dotnet add package Swashbuckle.AspNetCore
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;

// Chamar as bibliotecas do Entity Framework
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using exercicio_api.Database;

namespace exercicio_api
{
    public class Executar
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}