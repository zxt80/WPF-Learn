using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using MyToDo.api.Context;
using MyToDo.api.Service;

namespace MyToDo.api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService toDoService;

        public ToDoController(IToDoService toDoService)
        {
            this.toDoService = toDoService;
        }

        [HttpGet]
        public async Task<APIResponse> Get(int id) => await this.toDoService.GetSingleAsync(id);
        [HttpGet]
        public async Task<APIResponse> GetAll() => await this.toDoService.GetAllAsync();

        [HttpPost]
        public async Task<APIResponse> Add([FromBody] ToDo toDo)=>await this.toDoService.AddAsync(toDo);

        [HttpPost]
        public async Task<APIResponse> Update([FromBody] ToDo toDo) => await this.toDoService.UpdateAsync(toDo);

        [HttpDelete]
        public async Task<APIResponse> Update(int id) => await this.toDoService.DeleteAsync(id);

    }
}
