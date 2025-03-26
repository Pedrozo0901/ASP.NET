using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exemplo_aspnet_endpoint.Models;


// Para fazer os protocolos HTTP, precisamos de um controller da biblioteca do AS.NET, que é MVC o qual lá tem o Controller do ASP.NET
using Microsoft.AspNetCore.Mvc;
// dotnet add package Microsoft.AspNetCore.MVC

namespace exemplo_aspnet_endpoint.Controller
{
    // Vamos adicionar um Anotation para dizer que essa classe é um controller para referenciar os endpoints e métodos HTTP
    [ApiController]
    // vamos adicionar um anotation para dizer que essa classe é um controller para referenciar os endpoints e métodos HTTP, definindo a rota para cessar esse controller
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private static List<Usuario> usuarios = new List<Usuario>()
        {
            new Usuario {Id = 1, Nome = "João", Email = "joao@email.com"},
            new Usuario {Id = 2, Nome = "Maria", Email = "maria@email.com"},
            new Usuario {Id = 3, Nome = "Pedro", Email = "pedro@email.com"}
        };

        // Vamo chamar o anotation para definir a rota para acessar esse metodo de requisição HTTP para listar os usuarios
        [HttpGet]
        public IEnumerable<Usuario> Get() // IEnumerable é uma interace que representa uma coleção de objetos que podem ser iterados
        {
            return usuarios;
        }

        [HttpPost]
        public Usuario Post([FromBody] Usuario usuario) // [FromBody] é um atributo que indica que um parâmetro de ação deve ser ligado ao corpo da solicitação HTTP. O atributo [FromBody] informa ao MVC para o valor do corpo da solicitação HTTP e liga-lo ap parâmetro do método
        {
            usuarios.Add(usuario); // vai adicionar o usuario na lista de usuarios
            return usuario; // vai retornar o usuario
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Usuario usuario) //
        {
            var usuarioExistente = usuarios.Where( x => x.Id == id).FirstOrDefault();

            if(usuarioExistente != null)
            {
                usuarioExistente.Nome = usuario.Nome;
                usuarioExistente.Email = usuario.Email;
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var usuarioExistente = usuarios.Where(x => x.Id == id).FirstOrDefault();

            if(usuarioExistente != null)
            {
                usuarios.Remove(usuarioExistente);
            }
        }
    }
}