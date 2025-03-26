using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using exemplo_aspnet_endpoint.Controller;

// Para executar os comando, é preciso instalar os pacotes ASP.NET Core com o comando:
// dotnet app package Microsoft.AspNetCore

// Será usado a ferramente Swagger para documentar a API, que já está incluida no ASP.NET Core, mas precisa de um pacote para funcionar, e nisso é preciso executar o comando:
// dotnet add package Swashbuckle.AspNetCore

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace exemplo_aspnet_endpoint
{
    public class Executar
    {
        public static void Main(string[] args)
        {
            // Vamos chamar uma classe selead com nome  WebApplication, que é uma classe que presenta um aplicação web ASP.NET Core
            var builder = WebApplication.CreateBuilder(args); // Vai criar a aplicação web

            // Agora vou adicionar os serviços de controler do WebApplication 
            builder.Services.AddControllers();

            // Habilitar o Swagger para documentação da API
            builder.Services.AddEndpointsApiExplorer();

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