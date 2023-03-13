using Microsoft.AspNetCore.Mvc;
using MyToDo.api.Service;
using MyToDo.shared.Dtos;
using MyToDo.shared;

namespace MyToDo.api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService userService;

        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<ApiResponse> Login([FromBody] UserDto userDto) => await this.userService.Login(userDto.Account, userDto.Passwd);

        [HttpPost]
        public async Task<ApiResponse> Regist([FromBody] UserDto userDto) => await this.userService.Regist(userDto);

    }
}
