using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace JhonnySe.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _client;

        public HttpClientService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
        }

        public void ClearHeaders()
        {
            _client.DefaultRequestHeaders.Clear();
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _client.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return await _client.PostAsync(url, content);
        }

        public void SetAcceptHeader(string type)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(type));
        }

        public void SetAuthHeader(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public void SetBasicAuthHeader(string clientId, string clientSecret)
        {
            var base64Encoded = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{clientId}:{clientSecret}"));
            var auth = new AuthenticationHeaderValue("Basic", base64Encoded);
            _client.DefaultRequestHeaders.Authorization = auth;
        }

        public void SetHeader(string key, string value)
        {
            _client.DefaultRequestHeaders.Add(key, value);
        }

        public void SetHeaders(Dictionary<string, string> headers)
        {
            foreach(var header in headers)
            {
                _client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        public static MemoryStream CreateMemoryStreamFromObject<T>(T model)
        {
            var serializedString = JsonConvert.SerializeObject(model);
            byte[] bytes = Encoding.UTF8.GetBytes(serializedString);
            return new MemoryStream(bytes);
        }
    }
}
