using MyToDo.shared;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyToDo.Service
{
    public class HttpRestClient
    {
        private readonly string _apiUrl;

        public HttpRestClient(string apiUrl) 
        {
            this._apiUrl = apiUrl;
        }

        public async Task<ApiResponse> ExcluteAsync(BaseRequest parameter)
        {
            var request = new RestRequest();
            request.AddHeader("Content-Type", parameter.ContentType);

            if(parameter.Parameter!=null)
                request.AddParameter(
                    "param", JsonConvert.SerializeObject(parameter.Parameter), 
                    ParameterType.RequestBody);

            var client = new RestClient(_apiUrl + parameter.Route);
            
            var response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<ApiResponse>(response.Content);
        }

        public async Task<ApiResponse<T>> ExcluteAsync<T>(BaseRequest parameter)
        {
            var request = new RestRequest();
            request.Method = parameter.Method;
            request.AddHeader("Content-Type", parameter.ContentType);

            if (parameter.Parameter != null)
                request.AddParameter(
                    "param", JsonConvert.SerializeObject(parameter.Parameter),
                    ParameterType.RequestBody);

            string url = _apiUrl + parameter.Route;
            RestClient client = new RestClient(url);


            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ApiResponse<T> data = JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
                return data;
            }
            else
                return new ApiResponse<T>(response.ErrorMessage);
                
        }
    }
}
