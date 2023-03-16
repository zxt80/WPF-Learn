using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.shared.Parameters
{
    public class ToDoParameter:QueryParameters
    {
        public int? Status { get; set; }
    }
}
