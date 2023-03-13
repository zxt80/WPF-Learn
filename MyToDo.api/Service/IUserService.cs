using MyToDo.api.Context;
using MyToDo.shared;
using MyToDo.shared.Dtos;

namespace MyToDo.api.Service
{
    public interface IUserService 
    {
        Task<ApiResponse> Login(string account, string passwd);
        Task<ApiResponse> Regist(UserDto userDto);
    }
}
