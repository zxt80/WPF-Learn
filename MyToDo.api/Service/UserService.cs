using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.api.Context;
using MyToDo.shared;
using MyToDo.shared.Dtos;
using System.Reflection.Metadata;
using System.Security.Principal;

namespace MyToDo.api.Service
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Login(string account, string passwd)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<User>();
                var model = await repository.GetFirstOrDefaultAsync(
                    predicate: x =>x.Account.Equals(account) && x.Passwd.Equals(passwd));

                if (model == null)
                    return new ApiResponse("登录失败，用户名或密码错误！");
                else
                    return new ApiResponse(true,"登录成功");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.ToString());
            }
        }
        public async Task<ApiResponse> Regist(UserDto userDto)
        {
            try
            {
                User user = this._mapper.Map<User>(userDto);
                var repository = _unitOfWork.GetRepository<User>();
                var model = await repository.GetFirstOrDefaultAsync(
                    predicate: x => x.Account.Equals(user.Account));

                if (model != null)
                    return new ApiResponse("用户已存在，请重新注册！");
                
                user.CreateTime = DateTime.Now;

                await repository.InsertAsync(user);
                if (await _unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResponse(true, user);

                return new ApiResponse("注册失败，请稍后重试");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.ToString());
            }
        }
    }
}
