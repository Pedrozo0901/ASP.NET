using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Como usar o Entity Framework Core para acessar o banco de dados
// Vamos ter mapear o schema do banco de dados para classes C#.
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace exercicio_api.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        [Column("password")]
        public string Password {get; set;}
        [Column("nome_usuario")]
        [Required]
        public string Nome {get; set;}
        [Column("ramal")]
        public int Ramal {get; set;}
        [Column("especialidade")]
        public string Especialidade {get; set;}

    }
}