using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using exercicio_api.Models; // Importa o namespace do Model
using exercicio_api.Database; // Importa o namespace do DbContext

using Microsoft.EntityFrameworkCore; // Importa o namespace do Entity Framework

// Agente vai utilizar a biblioteca MVC do ASP.NET
using Microsoft.AspNetCore.Mvc; // O comando para instalar é: dotnet add package Microsoft.AspNetCore.Mvc

namespace exercicio_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Usuario>> Get()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();
            return usuario;
        }


        [HttpPost]
        public async Task<ActionResult<Usuario>> Post([FromBody] Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> Put(int id, [FromBody] Usuario usuario)
        {
            var existente = await _context.Usuarios.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Password = usuario.Password;
            existente.Nome = usuario.Nome;
            existente.Ramal = usuario.Ramal;
            existente.Especialidade = usuario.Especialidade;

            await _context.SaveChangesAsync();
            return existente;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existente = await _context.Usuarios.FindAsync(id);
            if(existente == null) return NotFound();

            _context.Usuarios.Remove(existente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}