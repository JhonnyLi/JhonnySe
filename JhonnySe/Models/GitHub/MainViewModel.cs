using JhonnySe.Models.Words;
using System.Collections.Generic;

namespace JhonnySe.Models.GitHub
{
    public class MainViewModel
    {
        public string OwnerName { get; set; }
        public string avatar_url { get; set; }
        public string GitHubUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public List<RepositoryViewModel> Repositorys { get; set; }
        public Sentences Words { get; set; }
    }
}
