using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.shared.Parameters
{
    public class QueryParameters
    {
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public string? Search { set; get; } = string.Empty;
    }
}
