using MyToDo.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    internal class BaseServer<TEntity>:IBaseService<TEntity> where TEntity : class
    {
        protected readonly HttpRestClient _client;
        protected readonly string _serviceName;
        public BaseServer(HttpRestClient client, string serviceName) 
        {
            _client = client;
            _serviceName = serviceName;
        }

        public async Task<ApiResponse> AddAsync(TEntity t)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/{_serviceName}/Add";
            request.Parameter = t;
            return await _client.ExcluteAsync(request);
        }

        public Task<ApiResponse> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetAll(QueryParameters param)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetFristOfDefaultAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UpdateAsync(TEntity t)
        {
            throw new NotImplementedException();
        }
    }
}
