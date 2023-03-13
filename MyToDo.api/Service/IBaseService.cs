using MyToDo.shared;

namespace MyToDo.api.Service
{
    public interface IBaseService<T>
    {
        Task<ApiResponse> GetAllAsync(QueryParameters parameter);
        Task<ApiResponse> GetSingleAsync(int id);
        Task<ApiResponse> AddAsync(T model);
        Task<ApiResponse> UpdateAsync(T model);
        Task<ApiResponse> DeleteAsync(int id);
    }
}
