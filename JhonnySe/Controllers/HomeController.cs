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
        readonly ILinkedinRepository _linkedIn;
        public HomeController(IGitHubRepository gitHubRepo, ILinkedinRepository linkedIn)
        {
            _gitHub = gitHubRepo;
            _linkedIn = linkedIn;
        }
        public async Task<IActionResult> Index()
        {
            var model = await GetViewModel();
            
            return View(model);
        }

        private async Task<MainViewModel> GetViewModel()
        {
            var user = await _gitHub.GetUser("JhonnyLi").ConfigureAwait(false);
            var result = await _gitHub.GetReposFromUser(user).ConfigureAwait(false);
            var model = new MainViewModel();
            model.avatar_url = user.avatar_url;
            model.OwnerName = user.name;
            model.GitHubUrl = user.html_url;
            model.LinkedInUrl = _linkedIn.GetLinkedInProfileLink();
            model.Repositorys = result.Select(r => new RepositoryViewModel { 
                Name = r.name, 
                CreatedDate = r.created_at, 
                Description = r.description, 
                UpdatedAt = r.updated_at })
                .OrderByDescending(d => d.UpdatedAt).ToList();

            return model;
        }
    }
}