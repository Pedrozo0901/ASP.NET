using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;

namespace exercicio_api.Models
{
    [Table("software")]
    public class Software
    {
        [Column("id_software")]
        public int Id {get; set;}

        [Column("produto")]
        public string Produto {get; set;}

        [Column("harddisk")]
        public int HardDisk {get; set;}

        [Column("memoria_ram")]
        public int MemoriaRam {get; set;}

        [Column("fk_maquina")]
        public int FkMaquina {get; set;}
    }
}