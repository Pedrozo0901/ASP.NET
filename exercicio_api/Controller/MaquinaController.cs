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
    public class MaquinaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MaquinaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Maquina>> Get()
        {
            return await _context.Maquinas.ToListAsync();
        }

        [HttpGet("{id}")]
        
        public async Task<ActionResult<Maquina>> Get(int id)
        {
            var maquina = await _context.Maquinas.FindAsync(id);
            if (maquina == null) return NotFound();
            return maquina;
        }

        [HttpPost]
        public async Task<ActionResult<Maquina>> Post([FromBody] Maquina maquina)
        {
            _context.Maquinas.Add(maquina);
            await _context.SaveChangesAsync();
            return maquina;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Maquina>> Put(int id, [FromBody] Maquina maquina)
        {
            var existente = await _context.Maquinas.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Tipo = maquina.Tipo;
            existente.Velocidade = maquina.Velocidade;
            existente.HardDisk = maquina.HardDisk;
            existente.PlacaRede = maquina.PlacaRede;
            existente.MemoriaRam = maquina.MemoriaRam;

            await _context.SaveChangesAsync();
            return existente;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existente = await _context.Maquinas.FindAsync(id);
            if(existente == null) return NotFound();

            _context.Maquinas.Remove(existente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}