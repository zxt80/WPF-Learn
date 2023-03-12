using Arch.EntityFrameworkCore.UnitOfWork;
using MyToDo.api.Context;

namespace MyToDo.api.Service
{
    public class ToDoService : IToDoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToDoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse> AddAsync(ToDo model)
        {
            try
            {
                await _unitOfWork.GetRepository<ToDo>().InsertAsync(model);
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

        public async Task<APIResponse> UpdateAsync(ToDo model)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(model.Id));
                todo.Title = model.Title;
                todo.Context = model.Context;
                todo.Status = model.Status;
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
