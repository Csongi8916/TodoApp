using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Helpers
{
    public class TodoParams
    {
        // Pagination params:
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int pageSize = 25;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        // Filter params:
        public string Title { get; set; }
        public bool? IsFinished { get; set; }
    }
}
