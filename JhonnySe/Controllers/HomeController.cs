using JhonnySe.Models.GitHub;
using JhonnySe.Repositorys;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index()
        {
            var model = await GetViewModel();
            var content = BlobStorageClient.CreateMemoryStreamFromObject<MainViewModel>(model);
            //await _storage.UploadStream("Test", content);
            //model = null;
            //model = _storage.GetBlob<MainViewModel>();
            return View(model);
        }
        [HttpGet]
        public IActionResult ValidationEndpoint(string challengeCode)
        {
            return Ok("");
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