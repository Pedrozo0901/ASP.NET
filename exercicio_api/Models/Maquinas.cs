using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;

namespace exercicio_api.Models
{
    [Table("maquina")]
    public class Maquina
    {
        [Column("id_maquina")]
        public int Id {get; set;}

        [Column("tipo")]
        public string Tipo {get; set;}

        [Column("velocidade")]
        public int Velocidade {get; set;}

        [Column("harddisk")]
        public int HardDisk {get; set;}

        [Column("placa_rede")]
        public int PlacaRede {get; set;}

        [Column("memoria_ram")]
        public int MemoriaRam {get; set;}

        [Column("fk_usuario")]
        public int FkUsuario {get; set;}
    }
}