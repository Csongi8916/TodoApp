using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data;

namespace TodoApp.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DataContext _ctx;
        public TodoRepository(DataContext ctx)
        {
            _ctx = ctx;
        }

        public void Add(TodoItem todoItem)
        {
            _ctx.Todos.Add(todoItem);
        }

        public void Delete(TodoItem todoItem)
        {
            _ctx.Todos.Remove(todoItem);
        }

        public async Task<TodoItem> GetTodo(string id)
        {
            return await _ctx.Todos.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TodoItem>> GetTodos()
        {
            return await _ctx.Todos.ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}
