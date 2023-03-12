using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.api.Context;
using MyToDo.shared.Dtos;

namespace MyToDo.api.Service
{
    public class ToDoService : IToDoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ToDoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<APIResponse> AddAsync(ToDoDto model)
        {
            try
            {
                var todo = _mapper.Map<ToDo>(model);
                await _unitOfWork.GetRepository<ToDo>().InsertAsync(todo);
                if (await _unitOfWork.SaveChangesAsync() > 0)
                    return new APIResponse(true, model);
                return new APIResponse("添加数据失败");
            }
            catch (Exception ex)
            {
                return new APIResponse(ex.ToString());
            }
        }

        public async Task<APIResponse> DeleteAsync(int id)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                repository.Delete(todo);

                if (await _unitOfWork.SaveChangesAsync() > 0)
                    return new APIResponse(true, "");
                return new APIResponse("删除数据失败");
            }
            catch (Exception ex)
            {
                return new APIResponse(ex.ToString());
            }
        }

        public async Task<APIResponse> GetAllAsync()
        {
            try
            {
                var repository = _unitOfWork.GetRepository<ToDo>();
                var todos = await repository.GetAllAsync();
                return new APIResponse(true, todos);
            }
            catch (Exception ex)
            {
                return new APIResponse(ex.ToString());
            }
        }

        public async Task<APIResponse> GetSingleAsync(int id)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return new APIResponse(true, todo);
            }
            catch (Exception ex)
            {
                return new APIResponse(ex.ToString());
            }
        }

        public async Task<APIResponse> UpdateAsync(ToDoDto model)
        {
            try
            {
                var todoOld = _mapper.Map<ToDo>(model);
                var repository = _unitOfWork.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(model.Id));
                todo.Title = todoOld.Title;
                todo.Content = todoOld.Content;
                todo.Status = todoOld.Status;
                todo.UpdateTime = DateTime.Now;
                repository.Update(todo);

                if ( await _unitOfWork.SaveChangesAsync() > 0)
                    return new APIResponse(true, todo);
                return new APIResponse("更新数据失败");
            }
            catch (Exception ex)
            {
                return new APIResponse(ex.ToString());
            }
        }
    }
}
