using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Dtos
{
    public class TodoDetailedDto
    { 
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFinished { get; set; }
    }
}
