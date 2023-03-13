using MyToDo.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public class BaseServer<TEntity>:IBaseService<TEntity> where TEntity : class
    {
        private readonly HttpRestClient _client;
        private readonly string _serviceName;
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

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"/api/{_serviceName}/Delete?id={id}";
            request.Parameter = null;
            return await _client.ExcluteAsync(request);
        }

        public async Task<ApiResponse<PagedList<TEntity>>> GetAllAsync(QueryParameters param)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"/api/{_serviceName}/GetAll?PageIndex={param.PageIndex}&PageSize={param.PageSize}";
            request.Parameter = null;
            return await _client.ExcluteAsync< PagedList < TEntity >> (request);
        }

        public async Task<ApiResponse<TEntity>> GetFristOfDefaultAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"/api/{_serviceName}/Delete?id={id}";
            request.Parameter = null;
            return await _client.ExcluteAsync<TEntity>(request);
        }

        public async Task<ApiResponse> UpdateAsync(TEntity t)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"/api/{_serviceName}/update";
            request.Parameter = t;
            return await _client.ExcluteAsync(request);
        }
    }
}
