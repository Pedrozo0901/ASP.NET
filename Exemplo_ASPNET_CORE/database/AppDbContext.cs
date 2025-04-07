using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Para fazer a ligação com o banco de dados, vamos usar o Entity Framework Core com comando:
using Microsoft.EntityFrameworkCore;



// os 3 comandos para rodar o entity framework core:
// dotnet add package Microsoft.EntityFrameworkCore
// dotnet add package Microsoft.EntityFrameworkCore.Design
// dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

namespace Exemplo_ASPNET_CORE.database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Models.Usuario> Usuarios {get;set;}
    }
}