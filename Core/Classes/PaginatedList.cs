using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Classes
{
    public  class PaginatedList<T>
    {
        public int PageSize { get; set; }   

        public int TotalCount { get; set; }

        public int PageIndex { get; set; }

        public IEnumerable<T> Items { get; set; } = null!;
    }
}
