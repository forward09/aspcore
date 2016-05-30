using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreProduct.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCoreProduct.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        public ITodoRepository ToDoItems;

        public TodoController(ITodoRepository todoItems)
        {
            ToDoItems = todoItems;
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            ToDoItems.Add(new TodoItem
            {
                Name = "Winfred Xu"
            });

            return ToDoItems.GetAll();
        }


        // GET api/values/5
        [HttpGet("{id}", Name ="GetTodo")]
        public IActionResult GetById(string id)
        {
            var item = ToDoItems.Find(id);

            if(item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            ToDoItems.Add(item);

            return CreatedAtRoute("GetToDo", new { controller = "Todo", id = item.Key }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] TodoItem item)
        {
            if (item == null || item.Key != id)
            {
                return BadRequest();
            }

            var todo = ToDoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            ToDoItems.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            ToDoItems.Remove(id);
        }
    }
}
