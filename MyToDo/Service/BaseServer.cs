using MyToDo.shared;
using MyToDo.shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Service
{
    public class BaseServer<TEntity>:IBaseService<TEntity> where TEntity : class
    {
        protected readonly HttpRestClient _client;
        private readonly string _serviceName;
        public BaseServer(HttpRestClient client, string serviceName)
        {
            _client = client;
            _serviceName = serviceName;
        }

        public async Task<ApiResponse<TEntity>> AddAsync(TEntity t)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"/api/{_serviceName}/Add";
            request.Parameter = t;
            return await _client.ExcluteAsync<TEntity>(request);
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.DELETE;
            request.Route = $"/api/{_serviceName}/Delete?id={id}";
            request.Parameter = null;
            return await _client.ExcluteAsync(request);
        }

        public async Task<ApiResponse<PagedList<TEntity>>> GetAllAsync(QueryParameters param)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.GET;
            request.Route = $"/api/{_serviceName}/GetAll?PageIndex={param.PageIndex}&PageSize={param.PageSize}";
            if(!string.IsNullOrEmpty( param.Search))
                request.Route +=$"&Search={param.Search}";
            return await _client.ExcluteAsync< PagedList < TEntity >> (request);
        }

        public async Task<ApiResponse<TEntity>> GetFristOfDefaultAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.GET;
            request.Route = $"/api/{_serviceName}/Delete?id={id}";
            request.Parameter = null;
            return await _client.ExcluteAsync<TEntity>(request);
        }

        public async Task<ApiResponse> UpdateAsync(TEntity t)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"/api/{_serviceName}/Update";
            request.Parameter = t;
            return await _client.ExcluteAsync(request);
        }
    }
}
