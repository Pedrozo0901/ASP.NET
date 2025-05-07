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
    // readonly é um modificador de acesso que indica que o campo _context é somente leitura e não pode ser modificado após a inicialização do objeto
    private readonly AppDbContext _context; // reandoly é um modificador de acesso que indica que o campo _context é somente leitura e não pode ser modificado após a inicialização do objeto
    // AppDbContext é uma classe que representa o contexto do banco de dados e fornece acesso às entidades do banco de dados

    public AlunoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlunoDTO>>> Get()
    // async é necessário para operações assíncronas
    // Task<ActionResult> é o tipo de retorno para métodos assíncronos
    // ActionResult é uma classe base para retornar resultados de ações em controladores ASP.NET Core
    // IEnumerable<AlunoDTO> é o tipo de dados que será retornado, representando uma coleção de objetos AlunoDTO
    // AlunoDTO é um objeto de transferência de dados (DTO) que representa um aluno
    // e contém as propriedades que queremos expor na API
    {
        var alunos = await _context.Alunos
            .Include(a => a.Curso)
            .Select(a => new AlunoDTO
            {
                Id = a.Id,
                Nome = a.Nome,
                Curso = a.Curso.Descricao
            })
            .ToListAsync();

        return Ok(alunos);
    }

    [HttpPost]
    // [FromBody] indica que o objeto AlunoDTO será passado no corpo da requisição HTTP
    // AlunoDTO é um objeto de transferência de dados (DTO) que representa um aluno
    // e contém as propriedades que queremos expor na API
    // ActionResult é uma classe base para retornar resultados de ações em controladores ASP.NET Core
    // Task<ActionResult> é o tipo de retorno para métodos assíncronos
    public async Task<ActionResult> Post([FromBody] AlunoDTO alunoDto)
    {
        var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == alunoDto.Curso);
        if (curso == null) return BadRequest("Curso não encontrado.");

        var aluno = new Aluno { Nome = alunoDto.Nome, CursoId = curso.Id };
        _context.Alunos.Add(aluno);
        await _context.SaveChangesAsync();

        return Ok(new { mensagem = "Aluno cadastrado com sucesso" }); // mensagem é propriedade anonima que contem um valor adtribuido
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] AlunoDTO alunoDto)
    // [FromBody] indica que o objeto AlunoDTO será passado no corpo da requisição HTTP
    // AlunoDTO é um objeto de transferência de dados (DTO) que representa um aluno
    // e contém as propriedades que queremos expor na API
    // ActionResult é uma classe base para retornar resultados de ações em controladores ASP.NET Core
    // Task<ActionResult> é o tipo de retorno para métodos assíncronos
    {
        var aluno = await _context.Alunos.FindAsync(id); // FindAsync é um método assíncrono que procura uma entidade pelo seu identificador
        // Aluno é uma classe que representa um aluno no banco de dados
        if (aluno == null) return NotFound("Aluno não encontrado."); // Se o aluno não for encontrado, retorna um erro 404 (Not Found)

        var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == alunoDto.Curso); // Primeiro, procura o curso pelo nome
        // FirstOrDefaultAsync é um método assíncrono que retorna o primeiro elemento que atende à condição especificada ou um valor padrão se nenhum elemento for encontrado
        if (curso == null) return BadRequest("Curso não encontrado."); // Se o curso não for encontrado, retorna um erro 400 (Bad Request)
        // Aluno é uma classe que representa um aluno no banco de dados

        aluno.Nome = alunoDto.Nome; // Atualiza o nome do aluno com o valor fornecido no DTO
        // Aluno é uma classe que representa um aluno no banco de dados 
        aluno.CursoId = curso.Id;   // Atualiza o ID do curso do aluno com o ID do curso encontrado

        _context.Alunos.Update(aluno); // Update é um método que atualiza a entidade no contexto do banco de dados
        await _context.SaveChangesAsync(); // SaveChangesAsync é um método assíncrono que salva as alterações feitas no contexto do banco de dados
        // Aluno é uma classe que representa um aluno no banco de dados

        return Ok(new { mensagem = "Aluno alterado com sucesso" });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    // [FromBody] indica que o objeto AlunoDTO será passado no corpo da requisição HTTP
    // AlunoDTO é um objeto de transferência de dados (DTO) que representa um aluno
    // e contém as propriedades que queremos expor na API
    // ActionResult é uma classe base para retornar resultados de ações em controladores ASP.NET Core
    // Task<ActionResult> é o tipo de retorno para métodos assíncronos
    {
        var aluno = await _context.Alunos.FindAsync(id); // FindAsync é um método assíncrono que procura uma entidade pelo seu identificador
        if (aluno == null) return NotFound("Aluno não encontrado."); // Se o aluno não for encontrado, retorna um erro 404 (Not Found)

        _context.Alunos.Remove(aluno); // Remove é um método que remove a entidade do contexto do banco de dados
        // Aluno é uma classe que representa um aluno no banco de dados
        await _context.SaveChangesAsync(); // SaveChanges

        return Ok(new { mensagem = "ALuno excluido com sucesso" });
    }

    // Nov metodo de para buscar aluno por ID
    [HttpGet("{id}")]
    public async Task<ActionResult<AlunoDTO>> Get(int id)
    {
        var aluno = await _context.Alunos
            .Include(a => a.Curso)
            .FirstOrDefaultAsync(a => a.Id == id);

            // FirstOrDefaultAsync é metodo assincrono que retorna o primeiro elemento que atende a condição atribuida a ele, ou valor padrão se nenhum ele for encontrado, que é 500
            // Include é metodo que inclui entidades relacioanadas na consulta, permitindo carregar dados relacionados (como o curso do aluno) junto com o aluno
        

        if (aluno == null)
        {
            return NotFound("Aluno não encontrado");
        }

        var alunoDto = new AlunoDTO
        {
            Id = aluno.Id,
            Nome = aluno.Nome,
            Curso = aluno.Curso.Descricao
        };

        return Ok(alunoDto);
    }
}
}