using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JhonnySe.Services
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
        void SetAcceptHeader(string type);
        void SetHeader(string key, string value);
        void SetHeaders(Dictionary<string, string> headers);
        void ClearHeaders();

        void SetAuthHeader(string token);

        void SetBasicAuthHeader(string clientId, string clientSecret);
    }
}
