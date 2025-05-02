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
    public class CursoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CursoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursoDTO>>> Get()
        {
            var cursos = await _context.Cursos
                .Select(cursos => new CursoDTO {
                    Id = cursos.Id,
                    Descricao = cursos.Descricao})
                .ToListAsync();
            return Ok(cursos);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CursoDTO cursoDTO)
        {
            var curso = new Curso{Descricao = cursoDTO.Descricao};
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return Ok(new {mensagem = "Curso cadastrado com sucesso!", curso = curso}); // Retorna um status 200 OK e a mensagem de sucesso
        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(int id, [FromBody] CursoDTO cursoDTO)
        {
            var curso = await _context.Cursos.FindAsync(id);

            if (curso == null) return NotFound("Curso não encontrado.");

            curso.Descricao = cursoDTO.Descricao;

            _context.Cursos.Update(curso);
            await _context.SaveChangesAsync();

            return Ok(new {mensagem = "Curso atualizado com sucesso!", curso = curso}); // Retorna um status 200 OK e a mensagem de sucesso
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);

            if (curso == null) return NotFound("Curso não encontrado.");

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDTO>> Get(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);

            if (curso == null) return NotFound("Curso não encontrado.");

            var cursoDTO = new CursoDTO
            {
                Id = curso.Id,
                Descricao = curso.Descricao
            };

            return Ok(cursoDTO);
        }
    }
}