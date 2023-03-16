using MyToDo.shared;
using MyToDo.shared.Dtos;
using MyToDo.shared.Parameters;
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
        public async Task<ApiResponse<PagedList<ToDoDto>>> GetAllFilterAsync(ToDoParameter param)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.GET;
            request.Route = $"/api/ToDo/GetAll" +
                $"?PageIndex={param.PageIndex}" +
                $"&PageSize={param.PageSize}" +
                $"&Status={param.Status}" +
                $"&Search={param.Search}";
            return await _client.ExcluteAsync<PagedList<ToDoDto>>(request);
        }
    }
}
