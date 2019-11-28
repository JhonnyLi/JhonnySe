using JhonnySe.Models.GitHub;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using Octokit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JhonnySe.Repositorys
{
    public class GitHubRepository : IGitHubRepository
    {
        readonly ISecretsRepository _secretsRepository;
        readonly string _clientId;
        readonly string _clientSecret;
        readonly HttpClient _client;
        public GitHubRepository(ISecretsRepository secrets)
        {
            _secretsRepository = secrets;
            _clientId = _secretsRepository.GetSecret("GitHubApiClientId");
            _clientSecret = _secretsRepository.GetSecret("GitHubApiClientSecret");
            _client = GetClient();
        }

        public async Task<List<Repository>> GetReposFromUser(GitHubUser user)
        {
            var uri = new Uri(user.repos_url);
            var response = await _client.GetAsync(uri).ConfigureAwait(false);
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Repository>>(contentString);
        }

        public async Task<GitHubUser> GetUser(string user)
        {
            var uri = new Uri($"https://api.github.com/users/{user}");
            var response = await _client.GetAsync(uri).ConfigureAwait(false);
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GitHubUser>(contentString);
        }

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_clientId, _clientSecret);
            client.DefaultRequestHeaders.Add("User-Agent", "JhonnySeGitHubApi");
            return client;
        }
    }
}
