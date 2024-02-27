using homework.Model;
using Microsoft.AspNetCore.Mvc;

namespace homework.Controllers
{
    [Route("todos")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        static List<ToDo> ToDoList = new List<ToDo>()
        { new ToDo{Id = 1, Label = "todo1"},
          new ToDo{Id = 2, Label = "todo2"},
          new ToDo{Id = 3, Label = "todo3"},
          new ToDo{Id = 4, Label = "todo4"},
          new ToDo{Id = 5, Label = "todo5", IsDone = true},
          new ToDo{Id = 6, Label = "todo6", IsDone = true},
          new ToDo{Id = 7, Label = "todo7", IsDone = true},
          new ToDo{Id = 8, Label = "todo8", IsDone = true}};


        [HttpGet]
        public IActionResult Get(int limit, int offset)
        {
            if (offset >= ToDoList.Count) return NotFound();
            return Ok(ToDoList.Skip(offset).Take(limit).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ToDo? result = ToDoList.SingleOrDefault(x => x.Id == id);
            if (result == null) return NotFound(id);
            return Ok(result);
        }

        [HttpGet("{id}/IsDone")]
        public IActionResult GetIsDone(int id)
        {
            ToDo? result = ToDoList.SingleOrDefault(x => x.Id == id);
            if (result == null) return NotFound(id);
            return Ok(new IsDoneResult() {Id = id, IsDone = result.IsDone });
        }

        [HttpPost]
        public IActionResult Post([FromBody] ToDo todo)
        {
            int idNew = ToDoList.Max(x => x.Id) + 1;
            ToDoList.Add(new ToDo {Id = idNew, Label = todo.Label, IsDone = todo.IsDone});
            return Created("/todos/" + idNew, ToDoList.Where(i => i.Id == idNew).Single());
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ToDo todo)
        {
            if (id != todo.Id && ToDoList.Where(x=>x.Id == todo.Id).Any()) return Conflict();

            ToDo? result = ToDoList.SingleOrDefault(x => x.Id == id);
            if (result == null) return NotFound(id);

            result.Id = todo.Id;
            result.Label = todo.Label;
            result.IsDone = todo.IsDone;
            result.UpdatedTime = DateTime.UtcNow;
            return  Ok(result);
        }

        [HttpPatch("{id}/IsDone")]
        public IActionResult Patch(int id, [FromBody] bool isDone)
        {
            ToDo? result = ToDoList.SingleOrDefault(x => x.Id == id);
            if (result == null) return NotFound(id);

            result.IsDone = isDone;
            return Ok(new IsDoneResult() {Id = id, IsDone = isDone });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ToDo? result = ToDoList.SingleOrDefault(x => x.Id == id);
            if (result == null) return NotFound(id);

            ToDoList.Remove(result);
            return Ok();
        }
    }

}
