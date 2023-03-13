using MyToDo.shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public class ToDoService : BaseServer<ToDoDto>,IToDoService
    {
        public ToDoService(HttpRestClient client) : base(client, "ToDo")
        {
        }
    }
}
