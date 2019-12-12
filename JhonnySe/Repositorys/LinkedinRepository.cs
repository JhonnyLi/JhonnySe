using JhonnySe.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JhonnySe.Repositorys
{
    public class LinkedinRepository : ILinkedinRepository
    {
        private readonly static string MediaTypeFormUrlEncoded = "application/x-www-form-urlencoded";
        private readonly static string BaseUrl = "https://www.linkedin.com";
        private readonly static string OauthTokenUrl = BaseUrl + "/oauth/v2/accessToken";
        readonly ISecretsRepository _secretsRepository;
        readonly IHttpClientService _client;
        readonly string _clientId;
        readonly string _clientSecret;
        public LinkedinRepository(ISecretsRepository secrets, IHttpClientService client)
        {
            _secretsRepository = secrets;
            _client = client;
            _clientId = secrets.GetSecret("LinkedinClientId");
            _clientSecret = secrets.GetSecret("LinkedinClientSecret");
        }

        public async Task<string> GetAuthToken()
        {
            _client.ClearHeaders();
            _client.SetAcceptHeader(MediaTypeFormUrlEncoded);
            var headers = new Dictionary<string, string>
                {
                    { "grant_type", "client_credentials" },
                    { "client_id", _clientId },
                    { "client_secret", _clientSecret }
                };
            _client.SetHeaders(headers);
            var keyValues = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("client_secret", _clientSecret)
            };
            var content = new FormUrlEncodedContent(keyValues);
            var result = await _client.PostAsync(OauthTokenUrl, content);
            var token = await result.Content.ReadAsStringAsync();
            return token;
        }

        public string GetLinkedInProfileLink()
        {
            return _secretsRepository.GetSecret("LinkedinProfileLink");
        }

        public void SetBearerToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
