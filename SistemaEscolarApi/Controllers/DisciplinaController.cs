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
    public class DisciplinaController : ControllerBase
    {
        public readonly AppDbContext _context;

        public DisciplinaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisciplinaDTO>>> Get() // async é necessário para operações assíncronas
    // Task<ActionResult> é o tipo de retorno para métodos assíncronos
    // ActionResult é uma classe base para retornar resultados de ações em controladores ASP.NET Core
    // IEnumerable<DisciplinaDTO> é o tipo de dados que será retornado, representando uma coleção de objetos DisciplinaDTO
    // DisciplinaDTO é um objeto de transferência de dados (DTO) que representa uma disciplina
    // e contém as propriedades que queremos expor na API
    {
        var disciplinas = await _context.Disciplinas
            .Include(d => d.Curso) // Aqui é onde incluímos o curso associado à disciplina, ele precias de ser carregado junto com a disciplina
            .Select(d => new
            { // era para estar objeto DisciplinaDTO, mas como não tem o ID, não pode ser usado
                id = d.Id, // id é uma propriedade do objeto DisciplinaDTO que representa o ID da disciplina
                Descricao = d.Descricao,
                Curso = d.Curso.Descricao
            })
            .ToListAsync();

        return Ok(disciplinas);
    }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DisciplinaDTO dto)
    // [FromBody] indica que o objeto DisciplinaDTO será passado no corpo da requisição HTTP
    // DisciplinaDTO é um objeto de transferência de dados (DTO) que representa uma disciplina
    // e contém as propriedades que queremos expor na API
    // ActionResult é uma classe base para retornar resultados de ações em controladores ASP.NET Core
    // Task<ActionResult> é o tipo de retorno para métodos assíncronos
    {
        var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == dto.Curso);
        if (curso == null) return BadRequest("Curso não encontrado.");

        var disciplina = new Disciplina { Descricao = dto.Descricao, CursoId = curso.Id };
        _context.Disciplinas.Add(disciplina);
        await _context.SaveChangesAsync();

        return Ok(new { mensagem = "Disciplina cadastrada com sucesso" });
    }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DisciplinaDTO disciplinaDTO)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);

            if (disciplina == null) return NotFound("Disciplina não encontrada.");

            disciplina.Descricao = disciplinaDTO.Descricao;

            _context.Disciplinas.Update(disciplina);
            await _context.SaveChangesAsync();

            return Ok(new {mensagem = "Disciplina atualizada com sucesso!", disciplina = disciplina}); // Retorna um status 200 OK e a mensagem de sucesso
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);

            if (disciplina == null) return NotFound("Disciplina não encontrada.");

            _context.Disciplinas.Remove(disciplina);
            await _context.SaveChangesAsync();

            return Ok(new {mensagem = "Disciplina removida com sucesso!"}); // Retorna um status 200 OK e a mensagem de sucesso
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DisciplinaDTO>> GetById(int id)
        {
            var disciplinaDTO = await _context.Disciplinas
            .Where(d => d.Id == id)
            .Select(d => new DisciplinaDTO {
                Id = d.Id,
                Descricao = d.Descricao,
                Curso = d.Curso.Descricao
            })
            .FirstOrDefaultAsync();

            if(disciplinaDTO == null) return NotFound("Disciplina não encontrada!");

            return Ok(disciplinaDTO);
        }
    }

    
}