using MyToDo.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public interface IBaseService<T> where T : class
    {
        Task<ApiResponse> AddAsync(T t);
        Task<ApiResponse> UpdateAsync(T t);
        Task<ApiResponse> DeleteAsync(int id);

        Task<ApiResponse> GetFristOfDefaultAsync(int id);

        Task<ApiResponse> GetAll(QueryParameters param);
    }
}
