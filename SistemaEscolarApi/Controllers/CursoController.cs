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
            .Select(c => new CursoDTO
            {
                Id = c.Id,
                Descricao = c.Descricao
            })
            .ToListAsync();

        return Ok(cursos);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CursoDTO cursoDto)
    {
        var curso = new Curso { Descricao = cursoDto.Descricao };
        _context.Cursos.Add(curso);
        await _context.SaveChangesAsync();

        return Ok(new { mensagem = "Curso cadastrado com sucesso" });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] CursoDTO cursoDto)
    {
        var curso = await _context.Cursos.FindAsync(id);
        if (curso == null)
        {
            return NotFound();
        }

        curso.Descricao = cursoDto.Descricao;
        await _context.SaveChangesAsync();

        return NoContent(); // 204 No Content, é quando a operação foi bem sucedida mas não há conteúdo para retornar
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var curso = await _context.Cursos.FindAsync(id);
        if (curso == null)
        {
            return NotFound();
        }

        _context.Cursos.Remove(curso);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CursoDTO>> GetById(int id)
    {
        var curso = await _context.Cursos.FindAsync(id);
        if (curso == null) 
        {
            return NotFound();
        }

        var cursoDto = new CursoDTO
        {
            Id = curso.Id,
            Descricao = curso.Descricao
        };

        return Ok(cursoDto);
    }
}
}