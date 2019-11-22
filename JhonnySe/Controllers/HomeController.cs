using JhonnySe.Models.GitHub;
using JhonnySe.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JhonnySe.Controllers
{
    public class HomeController : Controller
    {
        readonly IGitHubRepository _gitHub;
        public HomeController(IGitHubRepository gitHubRepo)
        {
            _gitHub = gitHubRepo;
        }
        public async Task<IActionResult> Index()
        {
            var model = await GetViewModel();
            
            return View(model);
        }

        private async Task<MainViewModel> GetViewModel()
        {
            var user = await _gitHub.GetUser("JhonnyLi").ConfigureAwait(false);
            var result = await _gitHub.GetReposFromUser("JhonnyLi").ConfigureAwait(false);
            var model = new MainViewModel();
            model.avatar_url = user.avatar_url;
            model.OwnerName = user.name;
            model.GitHubUrl = user.html_url;
            model.Repositorys = result.Select(r => new RepositoryViewModel { Name = r.name, CreatedDate = r.created_at, Description = r.description }).OrderBy(d => d.CreatedDate).ToList();

            return model;
        }
    }
}