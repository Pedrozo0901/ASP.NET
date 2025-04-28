using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarApi.Models
{
    public class Disciplina
    {
        public int Id {get; set;}
        public string Descricao {get; set;}
        public int CursoId {get; set;}
        public Curso Curso {get; set;}
        
        public ICollection<DisciplinaAlunoCurso> DisciplinasAlunosCursos {get; set;}
    }
}