using MyToDo.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<ApiResponse> AddAsync(TEntity t);
        Task<ApiResponse> UpdateAsync(TEntity t);
        Task<ApiResponse> DeleteAsync(int id);

        Task<ApiResponse<TEntity>> GetFristOfDefaultAsync(int id);

        Task<ApiResponse<PagedList<TEntity>>> GetAllAsync(QueryParameters param);
    }
}
