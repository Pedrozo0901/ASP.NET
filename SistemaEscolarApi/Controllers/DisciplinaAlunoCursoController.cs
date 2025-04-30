using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolarApi.Models;
using SistemaEscolarApi.DTO;
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
            var registros = await _context.DisciplinasAlunosCursos
                .Select(d => new DisciplinaAlunoCursoDTO
                {
                    AlunoId = d.AlunoId,
                    CursoId = d.CursoId,
                    DisciplinaId = d.DisciplinaId
                })
                .ToListAsync();

            return Ok(registros);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DisciplinaAlunoCursoDTO dto)
        {
            var entidade = new DisciplinaAlunoCurso
            {
                AlunoId = dto.AlunoId,
                CursoId = dto.CursoId,
                DisciplinaId = dto.DisciplinaId
            };

            _context.DisciplinasAlunosCursos.Add(entidade);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DisciplinaAlunoCursoDTO dto)
        {
            var entidade = await _context.DisciplinasAlunosCursos.FindAsync(id);
            if (entidade == null)
            {
                return NotFound();
            }

            entidade.AlunoId = dto.AlunoId;
            entidade.CursoId = dto.CursoId;
            entidade.DisciplinaId = dto.DisciplinaId;

            _context.DisciplinasAlunosCursos.Update(entidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entidade = await _context.DisciplinasAlunosCursos.FindAsync(id);
            if (entidade == null)
            {
                return NotFound();
            }

            _context.DisciplinasAlunosCursos.Remove(entidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
