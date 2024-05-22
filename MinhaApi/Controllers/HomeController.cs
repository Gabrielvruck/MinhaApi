using Microsoft.AspNetCore.Mvc;
using MinhaApi.Data;
using MinhaApi.Models;

namespace MinhaApi.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Get([FromServices] AppDbContext context)
            => Ok(context.Todos.ToList());


        [HttpGet("/{id:int}")]
        public IActionResult GetById([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var todo = context.Todos.FirstOrDefault(x => x.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost("/")]
        public IActionResult Post([FromServices] AppDbContext context, [FromBody] Todo todo)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
            return Created($"/{todo.Id}", todo);
        }


        [HttpPut("/{id:int}")]
        public IActionResult Put([FromServices] AppDbContext context, [FromRoute] int id, [FromBody] Todo todo)
        {
            var entidade = context.Todos.FirstOrDefault(x => x.Id == id);
            if (entidade == null)
                return NotFound();

            context.Todos.Update(todo);
            context.SaveChanges();
            return Ok(todo);
        }


        [HttpPost("/")]
        public IActionResult Delete([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var entidade = context.Todos.FirstOrDefault(x => x.Id == id);
            if (entidade == null)
                return NotFound();

            context.Todos.Remove(entidade);
            context.SaveChanges();
            return Ok();
        }
    }
}
