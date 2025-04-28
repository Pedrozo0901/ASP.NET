using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarApi.Models
{
    public class Aluno
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        public int CursoId {get; set;}
        public Curso Curso {get; set;}


        // ICollection é uma coleção que pode ser usada para armazenar vários objetos do mesmo tipo.
        // Assim é a coleção de disciplinas que o aluno está matriculado.
        public ICollection<DisciplinaAlunoCurso> DisciplinasAlunosCursos {get; set;} 
    }
}