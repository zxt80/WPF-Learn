namespace MyToDo.api.Service
{
    public interface IBaseService<T>
    {
        Task<APIResponse> GetAllAsync();
        Task<APIResponse> GetSingleAsync(int id);
        Task<APIResponse> AddAsync(T model);
        Task<APIResponse> UpdateAsync(T model);
        Task<APIResponse> DeleteAsync(int id);
    }
}
