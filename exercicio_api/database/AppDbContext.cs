using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Para fazer a ligação com o banco de dados, vamos usar o Entity Framework Core
using Microsoft.EntityFrameworkCore; // Importa o namespace do Entity Framework

using exercicio_api.Models;


// Os 3 comandos para rodar entity framework core:
// dotnet add package Microsoft.EntityFrameworkCore
// dotnet add package Microsoft.EntityFrameworkCore.Design
// dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

namespace exercicio_api.Database 
{
    
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Maquina> Maquinas { get; set; }
        public DbSet<Software> Softwares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("usuarios");
            modelBuilder.Entity<Maquina>().ToTable("maquina");
            modelBuilder.Entity<Software>().ToTable("software");
        }
    }
}