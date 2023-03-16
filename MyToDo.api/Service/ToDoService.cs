using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using MyToDo.api.Context;
using MyToDo.shared;
using MyToDo.shared.Dtos;
using MyToDo.shared.Parameters;

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
        public async Task<ApiResponse> AddAsync(ToDoDto model)
        {
            try
            {
                var todo = _mapper.Map<ToDo>(model);
                await _unitOfWork.GetRepository<ToDo>().InsertAsync(todo);
                if (await _unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResponse(true, todo);
                return new ApiResponse("添加数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.ToString());
            }
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                repository.Delete(todo);

                if (await _unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResponse(true, "");
                return new ApiResponse("删除数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.ToString());
            }
        }

        public async Task<ApiResponse> GetAllAsync(QueryParameters parameter)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<ToDo>();
                var todos = await repository.GetPagedListAsync(predicate:
                x => string.IsNullOrEmpty(parameter.Search) ? true : x.Title.Contains(parameter.Search),
                pageIndex: parameter.PageIndex,
                    pageSize: parameter.PageSize,
                    orderBy: source => source.OrderByDescending(t => t.CreateTime));
                return new ApiResponse(true, todos);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.ToString());
            }
        }

        public async Task<ApiResponse> GetAllFilterAsync(ToDoParameter parameter)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<ToDo>();
                var todos = await repository.GetPagedListAsync(predicate:
                (x => (
                (string.IsNullOrEmpty(parameter.Search) ? true : x.Title.Contains(parameter.Search)) && 
                (parameter.Status!=null?(parameter.Status==x.Status):true))
                ),
                pageIndex: parameter.PageIndex,
                    pageSize: parameter.PageSize,
                    orderBy: source => source.OrderByDescending(t => t.CreateTime));
                return new ApiResponse(true, todos);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.ToString());
            }
        }

        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return new ApiResponse(true, todo);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.ToString());
            }
        }

        public async Task<ApiResponse> UpdateAsync(ToDoDto model)
        {
            try
            {
                var todoOld = _mapper.Map<ToDo>(model);
                var repository = _unitOfWork.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(model.Id));
                if(todo!=null)
                {
                    todo.Title = todoOld.Title;
                    todo.Content = todoOld.Content;
                    todo.Status = todoOld.Status;
                    todo.UpdateTime = DateTime.Now;
                    repository.Update(todo);

                    if (await _unitOfWork.SaveChangesAsync() > 0)
                        return new ApiResponse(true, todo);                    
                }
                return new ApiResponse("更新数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.ToString());
            }
        }
    }
}
