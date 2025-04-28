using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarApi.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public ICollection<DisciplinaAlunoCurso> DisciplinasAlunosCursos { get; set; } 

        
    }
}