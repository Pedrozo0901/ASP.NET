using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarApi.Models
{
    public class DisciplinaAlunoCurso
    {
        public int AlunoId {get; set;}
        public Aluno Aluno {get; set;}

        public int DisciplinaId {get; set;}
        public Disciplina Disciplina {get; set;}
        
        public int CursoId {get; set;}
        public Curso Curso {get; set;}
    }
}