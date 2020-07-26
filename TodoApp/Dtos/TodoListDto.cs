using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Dtos
{
    public class TodoListDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool IsFinished { get; set; }
    }
}
