using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Dtos;
using TodoApp.Helpers;
using TodoApp.Repositories;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _repo;
        private readonly IMapper _mapper;

        public TodoController(ITodoRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]TodoParams todoParams)
        {
            var todos = await _repo.GetTodos(todoParams);
            var todoDtos = _mapper.Map<IEnumerable<TodoListDto>>(todos);
            Response.AddPagination(todos.CurrentPage, todos.PageSize, todos.TotalCount, todos.TotalPage);
            return Ok(todoDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var todo = await _repo.GetTodo(id);
            var todoDto = _mapper.Map<TodoDetailedDto>(todo);
            return Ok(todoDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo(TodoDetailedDto todoDetailedDto)
        {
            var todo = _mapper.Map<TodoItem>(todoDetailedDto);
            _repo.Add(todo);

            if (await _repo.SaveAll())
                return StatusCode(201);

            throw new Exception("Creating todo {id} failed on save!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(string id, TodoDetailedDto todoDetailedDto)
        {
            var todo = await _repo.GetTodo(id);
            _mapper.Map(todoDetailedDto, todo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception("Updating user {id} failed on save!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(string id)
        {
            var todo = await _repo.GetTodo(id);
            if (todo == null)
            {
                return NotFound();
            }

            _repo.Delete(todo);
            if (await _repo.SaveAll())
                return Ok();

            throw new Exception("Deleting user {id} failed on save!");
        }
    }
}