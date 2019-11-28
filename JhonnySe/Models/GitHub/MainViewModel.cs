using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JhonnySe.Models.GitHub
{
    public class MainViewModel
    {
        public string OwnerName { get; set; }
        public string avatar_url { get; set; }
        public string GitHubUrl { get; set; }
        public List<RepositoryViewModel> Repositorys { get; set; }
    }
}
