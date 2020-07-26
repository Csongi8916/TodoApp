using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Data
{
    public class Seed
    {
        public static void SeedTodos(DataContext ctx)
        {
            if (!ctx.Todos.Any())
            {
                var todoData = System.IO.File.ReadAllText("Data/data.json");
                var todoItems = JsonConvert.DeserializeObject<List<TodoItem>>(todoData);
                foreach (var todoItem in todoItems)
                {
                    todoItem.Id = Guid.NewGuid().ToString();
                    ctx.Todos.Add(todoItem);
                }

                ctx.SaveChanges();
            }
        }
    }
}
