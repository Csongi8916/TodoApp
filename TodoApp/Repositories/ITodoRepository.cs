using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Helpers;

namespace TodoApp.Repositories
{
    public interface ITodoRepository
    {
        void Add(TodoItem todoItem);

        void Delete(TodoItem todoItem);

        Task<bool> SaveAll();

        Task<PageList<TodoItem>> GetTodos(TodoParams todoParams);

        Task<TodoItem> GetTodo(string id);
    }
}
