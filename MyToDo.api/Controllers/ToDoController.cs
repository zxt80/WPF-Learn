using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using MyToDo.api.Context;
using MyToDo.api.Service;
using MyToDo.shared;
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
        public async Task<ApiResponse> Get(int id) => await this.toDoService.GetSingleAsync(id);
        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameters parameters) => await this.toDoService.GetAllAsync(parameters);

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] ToDoDto toDoDto)=>await this.toDoService.AddAsync(toDoDto);

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] ToDoDto toDoDto) => await this.toDoService.UpdateAsync(toDoDto);

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await this.toDoService.DeleteAsync(id);

    }
}
