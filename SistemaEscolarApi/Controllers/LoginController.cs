using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolarApi.Models;
using SistemaEscolarApi.DTO;
using SistemaEscolarApi.DB;
using SistemaEscolarApi.Services;
using FluentValidation;

namespace SistemaEscolarApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return BadRequest("Usuário e senha são obrigatórios.");
            }
            
            var users = new List<Usuario>
            {
                new Usuario { Username = "Pedro", Password = "123", Role = "Administrador" },
                new Usuario { Username = "João", Password = "123", Role = "Funcionário" },
            };

            var user = users.FirstOrDefault(u => u.Username == loginDto.Username && u.Password == loginDto.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Usuário ou senha inválidos." });
            }

            var token = TokenService.GenerateToken(user);
            return Ok(new {token});

        }               
    }
}