using MyToDo.api.Context;
using MyToDo.shared;
using MyToDo.shared.Dtos;
using MyToDo.shared.Parameters;

namespace MyToDo.api.Service
{
    public interface IToDoService :IBaseService<ToDoDto>
    {
        Task<ApiResponse> GetAllFilterAsync(ToDoParameter parameter);
    }
}
