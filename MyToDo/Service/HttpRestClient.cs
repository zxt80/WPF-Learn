using MyToDo.shared;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Policy;

namespace MyToDo.Service
{
    public class HttpRestClient
    {
        private readonly string _apiUrl;

        public HttpRestClient(string apiUrl) 
        {
            this._apiUrl = apiUrl;
        }

        public async Task<ApiResponse> ExcluteAsync(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);

            if (baseRequest.Parameter != null)
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);
            var url = new Uri(_apiUrl + baseRequest.Route);
            RestClient client = new RestClient(url);
            
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ApiResponse>(response.Content);

            else
                return new ApiResponse(response.ErrorMessage);

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
