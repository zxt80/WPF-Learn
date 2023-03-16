using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using MyToDo.api.Context;
using MyToDo.api.Service;
using MyToDo.shared;
using MyToDo.shared.Dtos;
using MyToDo.shared.Parameters;

namespace MyToDo.api.Controllers
{
    /// <summary>
    /// 待办事项接口
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService toDoService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="toDoService"></param>
        public ToDoController(IToDoService toDoService)
        {
            this.toDoService = toDoService;
        }


        /// <summary>
        /// 获取指定项
        /// </summary>
        /// <param name="id">指定待办事项ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse> Get(int id) => await this.toDoService.GetSingleAsync(id);
        
        
        /// <summary>
        /// 获取所有满足条件的待办事项
        /// </summary>
        /// <param name="parameters">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] ToDoParameter parameters) => await this.toDoService.GetAllFilterAsync(parameters);


        /// <summary>
        /// 添加待办事项
        /// </summary>
        /// <param name="toDoDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] ToDoDto toDoDto)=>await this.toDoService.AddAsync(toDoDto);


        /// <summary>
        /// 更新待办事项
        /// </summary>
        /// <param name="toDoDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] ToDoDto toDoDto) => await this.toDoService.UpdateAsync(toDoDto);



        /// <summary>
        /// 删除待办事项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await this.toDoService.DeleteAsync(id);

    }
}
