using Microsoft.AspNetCore.Mvc;
using MyToDo.api.Context;
using MyToDo.api.Service;
using MyToDo.shared;
using MyToDo.shared.Dtos;
using MyToDo.shared.Parameters;

namespace MyToDo.api.Controllers
{
    /// <summary>
    /// 备忘录接口
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MemoController : ControllerBase
    {
        private readonly IMemoService memoService;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="toDoService"></param>
        public MemoController(IMemoService toDoService)
        {
            this.memoService = toDoService;
        }


        /// <summary>
        /// 或者指定备忘录信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse> Get(int id) => await this.memoService.GetSingleAsync(id);


        /// <summary>
        /// 获取满足条件的备忘录信息
        /// </summary>
        /// <param name="parameter">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameters parameter) => await this.memoService.GetAllAsync(parameter);



        /// <summary>
        /// 新增备忘录信息
        /// </summary>
        /// <param name="memoDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] MemoDto memoDto) => await this.memoService.AddAsync(memoDto);


        /// <summary>
        /// 更新备忘录信息
        /// </summary>
        /// <param name="memoDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] MemoDto memoDto) => await this.memoService.UpdateAsync(memoDto);


        /// <summary>
        /// 删除备忘录信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await this.memoService.DeleteAsync(id);

    }
}
