using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using exercicio_api.Models; // Importa o namespace do Model
using exercicio_api.Database; // Importa o namespace do DbContext

using Microsoft.EntityFrameworkCore; // Importa o namespace do Entity Framework

// Agente vai utilizar a biblioteca MVC do ASP.NET
using Microsoft.AspNetCore.Mvc; // O comando para instalar é: dotnet add package Microsoft.AspNetCore.Mvc

namespace exercicio_api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SoftwareController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SoftwareController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Software>> Get()
        {
            return await _context.Softwares.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Software>> Get(int id)
        {
            var software = await _context.Softwares.FindAsync(id);
            if (software == null) return NotFound();
            return software;
        }

        [HttpPost]
        public async Task<ActionResult<Software>> Post([FromBody] Software software)
        {
            _context.Softwares.Add(software);
            await _context.SaveChangesAsync();
            return software;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Software>> Put(int id, [FromBody] Software software)
        {
            var existente = await _context.Softwares.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Produto = software.Produto;
            existente.HardDisk = software.HardDisk;
            existente.MemoriaRam = software.MemoriaRam;

            await _context.SaveChangesAsync();
            return existente;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existente = await _context.Softwares.FindAsync(id);
            if(existente == null) return NotFound();

            _context.Softwares.Remove(existente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}