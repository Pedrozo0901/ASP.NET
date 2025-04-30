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
    public class DisciplinaAlunoCursoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DisciplinaAlunoCursoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisciplinaAlunoCursoDTO>>> Get()
        {
            var disciplinas = await _context.DisciplinasAlunosCursos
                .Include(d => d.DisciplinaId)
                .Include(d => d.AlunoId)
                .Include(d => d.CursoId)
                .Select(d => new DisciplinaAlunoCursoDTO
                {
                    AlunoId = d.AlunoId,
                    CursoId = d.CursoId,
                    DisciplinaId = d.DisciplinaId
                })
                .ToListAsync();
            return Ok(disciplinas);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DisciplinaAlunoCursoDTO disciplinaAlunoCursoDTO)
        {
            var disciplinaAlunoCurso = new DisciplinaAlunoCurso
            {
                AlunoId = disciplinaAlunoCursoDTO.AlunoId,
                CursoId = disciplinaAlunoCursoDTO.CursoId,
                DisciplinaId = disciplinaAlunoCursoDTO.DisciplinaId
            };
            _context.DisciplinasAlunosCursos.Add(disciplinaAlunoCurso);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DisciplinaAlunoCursoDTO disciplinaAlunoCursoDTO)
        {
            var disciplinaAlunoCurso = await _context.DisciplinasAlunosCursos.FindAsync(id);

            if (disciplinaAlunoCurso == null) return NotFound("Disciplina não encontrada.");

            disciplinaAlunoCurso.AlunoId = disciplinaAlunoCursoDTO.AlunoId;
            disciplinaAlunoCurso.CursoId = disciplinaAlunoCursoDTO.CursoId;
            disciplinaAlunoCurso.DisciplinaId = disciplinaAlunoCursoDTO.DisciplinaId;

            _context.DisciplinasAlunosCursos.Update(disciplinaAlunoCurso);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var disciplinaAlunoCurso = await _context.DisciplinasAlunosCursos.FindAsync(id);

            if (disciplinaAlunoCurso == null) return NotFound("Disciplina não encontrada.");

            _context.DisciplinasAlunosCursos.Remove(disciplinaAlunoCurso);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}