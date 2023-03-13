using Microsoft.AspNetCore.Mvc;
using MyToDo.api.Context;
using MyToDo.api.Service;
using MyToDo.shared;
using MyToDo.shared.Dtos;

namespace MyToDo.api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MemoController : ControllerBase
    {
        private readonly IMemoService memoService;

        public MemoController(IMemoService toDoService)
        {
            this.memoService = toDoService;
        }

        [HttpGet]
        public async Task<ApiResponse> Get(int id) => await this.memoService.GetSingleAsync(id);
        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameters parameter) => await this.memoService.GetAllAsync(parameter);

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] MemoDto memoDto) => await this.memoService.AddAsync(memoDto);

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] MemoDto memoDto) => await this.memoService.UpdateAsync(memoDto);

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await this.memoService.DeleteAsync(id);

    }
}
