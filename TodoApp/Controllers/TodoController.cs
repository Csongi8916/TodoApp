using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Dtos;
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
        public async Task<IActionResult> Get()
        {
            var todos = await _repo.GetTodos();
            var todoDtos = _mapper.Map<IEnumerable<TodoListDto>>(todos);
            return Ok(todoDtos);
        }
    }
}