using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Como usar o Entity Framework Core para cessar o banco de dados
// Vamos ter que mapear o schema do banco para classes C#.
using System.ComponentModel.DataAnnotations.Schema;

namespace Exemplo_ASPNET_CORE.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Column("id_cliente")]
        public int Id {get; set;}
        [Column("nome")]
        public string Nome {get; set;}
        [Column("email")]
        public string Email {get; set;}
    }
}