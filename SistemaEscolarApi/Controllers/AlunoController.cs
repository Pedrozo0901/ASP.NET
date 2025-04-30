using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaEscolarApi.Models;
using SistemaEscolarApi.DTO;
using Microsoft.AspNetCore.Mvc;
using SistemaEscolarApi.DB;

namespace SistemaEscolarApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly AppDbContext _context;  // Injeção de dependência do contexto do banco de dados

        public AlunoController(AppDbContext context) // Construtor que recebe o contexto do banco de dados
        {
            _context = context; // Inicializa o contexto do banco de dados
        }

        [HttpGet] // Método para obter todos os alunos
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> Get()
        {
            var alunos = await _context.Alunos
                .Include(a => a.Curso)
                .Select(alunos => new AlunoDTO {Nome = alunos.Nome, Curso = alunos.Curso.Descricao})
                .ToListAsync();
            return Ok(alunos);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AlunoDTO alunoDTO) {
            var Curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == alunoDTO.Curso);
            if (Curso == null) return BadRequest("Curso não encontrado.");
            
            var aluno = new Aluno{ Nome = alunoDTO.Nome, CursoId = Curso.Id};
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AlunoDTO alunoDTO)
        {
            var aluno = await _context.Alunos.FindAsync(id); // Vai fazer a procura do aluno, ou seja da entidade pelo seu identificador

            if(aluno == null) return NotFound("Aluno não encontrado.");
            var Curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == alunoDTO.Curso);
            if (Curso == null) return BadRequest("Curso não encontrado.");

            aluno.Nome = alunoDTO.Nome;

            aluno.CursoId = Curso.Id;

            _context.Alunos.Update(aluno); // Atualiza o aluno no contexto
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados 

            return Ok(); // Retorna um status 200 OK
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id); // Procura o aluno pelo id

            if(aluno == null) return BadRequest("Aluno não encontrado."); // Se não encontrar, retorna um erro 400

            _context.Alunos.Remove(aluno); // Remove o aluno do contexto

            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados

            return Ok();
        }
    }
}