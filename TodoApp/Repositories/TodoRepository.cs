using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.Helpers;

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

        public async Task<PageList<TodoItem>> GetTodos(TodoParams todoParams)
        {
            var todos = _ctx.Todos.AsQueryable();

            if (todoParams.IsFinished.HasValue)
            {
                todos = todos.Where(t => todoParams.IsFinished.Value ? t.IsFinished : !t.IsFinished);
            }
            if (!string.IsNullOrEmpty(todoParams.Title))
            {
                todos = todos.Where(t => t.Title.Contains(todoParams.Title));
            }

            return await PageList<TodoItem>.CreateAsync(todos, todoParams.PageNumber, todoParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}
