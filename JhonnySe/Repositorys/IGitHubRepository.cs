using JhonnySe.Models.GitHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JhonnySe.Repositorys
{
    public interface IGitHubRepository
    {
        public Task<GitHubUser> GetUser(string user);
        public Task<List<Repository>> GetReposFromUser(string user);
    }
}
