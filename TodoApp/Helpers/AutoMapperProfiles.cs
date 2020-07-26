using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Dtos;

namespace TodoApp.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TodoItem, TodoListDto>();
            CreateMap<TodoItem, TodoDetailedDto>();

            CreateMap<TodoListDto, TodoItem>();
            CreateMap<TodoDetailedDto, TodoItem>();
        }
    }
}
