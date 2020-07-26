using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Repositories
{
    public interface ITodoRepository
    {
        void Add(TodoItem todoItem);

        void Delete(TodoItem todoItem);

        Task<bool> SaveAll();

        Task<IEnumerable<TodoItem>> GetTodos();

        Task<TodoItem> GetTodo(string id);
    }
}
