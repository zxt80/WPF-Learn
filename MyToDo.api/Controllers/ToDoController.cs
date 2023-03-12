using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using MyToDo.api.Context;
using MyToDo.api.Service;
using MyToDo.shared.Dtos;

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
        public async Task<APIResponse> Add([FromBody] ToDoDto toDoDto)=>await this.toDoService.AddAsync(toDoDto);

        [HttpPost]
        public async Task<APIResponse> Update([FromBody] ToDoDto toDoDto) => await this.toDoService.UpdateAsync(toDoDto);

        [HttpDelete]
        public async Task<APIResponse> Update(int id) => await this.toDoService.DeleteAsync(id);

    }
}
