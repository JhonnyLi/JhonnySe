using JhonnySe.Models.GitHub;
using JhonnySe.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace JhonnySe.Controllers
{
    public class HomeController : Controller
    {
        readonly IGitHubRepository _gitHub;
        readonly ILinkedinRepository _linkedIn;
        readonly IBlobStorageClient _storage;
        public HomeController(IGitHubRepository gitHubRepo, ILinkedinRepository linkedIn, IBlobStorageClient storage)
        {
            _gitHub = gitHubRepo;
            _linkedIn = linkedIn;
            _storage = storage;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ValidationEndpoint(string challengeCode)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetModel()
        {
            var model = await GetViewModel();
            return Ok(model);
        }

        private async Task<MainViewModel> GetViewModel()
        {
            //TODO: Add caching
            var model = _storage.GetBlob<MainViewModel>();
            var user = await _gitHub.GetUser("JhonnyLi").ConfigureAwait(false);
            var result = await _gitHub.GetReposFromUser(user).ConfigureAwait(false);
            model.avatar_url = user.avatar_url;
            model.OwnerName = user.name;
            model.GitHubUrl = user.html_url;
            model.LinkedInUrl = _linkedIn.GetLinkedInProfileLink();
            model.Repositorys = result.Select(r => new RepositoryViewModel { 
                Name = r.name, 
                CreatedDate = r.created_at, 
                Description = r.description, 
                UpdatedAt = r.updated_at
            })
                .OrderByDescending(d => d.UpdatedAt).ToList();

            return model;
        }
    }
}