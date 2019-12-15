using JhonnySe.Controllers;
using JhonnySe.Models.GitHub;
using JhonnySe.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace JhonnySe.Test
{
    public class HomeControllerTests
    {
        [Fact]
        public async Task GetModelShouldReturnStatusCodeOk()
        {
            //Arrange
            var mockRepoList = GetMockRepos();
            var mockUser = GetMockUser();
            var mockGithubRepo = new Mock<IGitHubRepository>();
            mockGithubRepo.Setup(u => u.GetUser("JhonnyLi")).Returns(Task.FromResult(mockUser));
            mockGithubRepo.Setup(r => r.GetReposFromUser(mockUser)).Returns(Task.FromResult(mockRepoList));

            var mockLinkedInRepo = new Mock<ILinkedinRepository>();
            mockLinkedInRepo.Setup(l => l.GetLinkedInProfileLink()).Returns("");

            var mockMainViewModel = GetMockBlobData();
            var mockBlobStorageClient = new Mock<IBlobStorageClient>();
            mockBlobStorageClient.Setup(b => b.GetBlob<MainViewModel>()).Returns(mockMainViewModel);

            var controller = new HomeController(mockGithubRepo.Object, mockLinkedInRepo.Object, mockBlobStorageClient.Object);

            //Act
            var result = await controller.GetModel().ConfigureAwait(false);

            //Assert
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
        }

        private static GitHubUser GetMockUser()
        {
            var mockUser = new GitHubUser
            {
                name = "JhonnyLi",
                avatar_url = "",
                html_url = ""
            };
            
            return mockUser;
        }

        private static List<Repository> GetMockRepos()
        {
            var mockRepoList = new List<Repository>
            {
                new Repository
                {
                    name = "TestRepo",
                    created_at = DateTime.Now,
                    description = "Description",
                    updated_at = DateTime.Now
                }
            };

            return mockRepoList;
        }

        private static MainViewModel GetMockBlobData()
        {
            return JsonConvert.DeserializeObject<MainViewModel>("{\"OwnerName\":\"Jhonny Li\",\"avatar_url\":\"https://avatars2.githubusercontent.com/u/12933635?v=4\",\"GitHubUrl\":\"https://github.com/JhonnyLi\",\"LinkedInUrl\":\"https://www.linkedin.com/in/jhonnyli\",\"Repositorys\":[{\"Name\":\"JhonnySe\",\"CreatedDate\":\"2019-11-20T20:33:42Z\",\"UpdatedAt\":\"2019-11-28T12:22:16Z\",\"Description\":\"Jhonny.se. A personal webpage which will hold information about myself and my GitHub endevours.\"},{\"Name\":\"PageDeletionLinkChecker\",\"CreatedDate\":\"2019-03-16T04:32:37Z\",\"UpdatedAt\":\"2019-11-21T23:24:33Z\",\"Description\":\"Umbraco: On deletion of a page the page-tree will be checked for pages linking to the page being deleted and notify the user before deletion.\"},{\"Name\":\"ShoppingListServer\",\"CreatedDate\":\"2017-04-19T10:21:17Z\",\"UpdatedAt\":\"2019-11-21T23:23:36Z\",\"Description\":\"A school assignment project that use SignalR to keep all logged on shoppinglists synchronized.\"},{\"Name\":\"ArpRouter\",\"CreatedDate\":\"2019-09-25T15:29:54Z\",\"UpdatedAt\":\"2019-11-21T23:21:15Z\",\"Description\":\"A csharp Arp-spoofer project.\"},{\"Name\":\"TimeKeeper\",\"CreatedDate\":\"2019-11-20T01:58:33Z\",\"UpdatedAt\":\"2019-11-21T23:15:43Z\",\"Description\":\"A small project to keep track of time spent on projects.\"},{\"Name\":\"SimpleHttpClient\",\"CreatedDate\":\"2019-03-09T12:59:05Z\",\"UpdatedAt\":\"2019-09-19T22:21:51Z\",\"Description\":\"Attempt at making it easier to use the HttpClient\"},{\"Name\":\"ZenView\",\"CreatedDate\":\"2019-03-14T21:34:05Z\",\"UpdatedAt\":\"2019-09-19T22:21:45Z\",\"Description\":\"A personal learning experience in converting Zendesk users and tickets to a digital overview board.\"},{\"Name\":\"AndroidShoppingList\",\"CreatedDate\":\"2017-03-08T20:46:53Z\",\"UpdatedAt\":\"2019-03-15T20:47:24Z\",\"Description\":\"(School project) A shopping-list that can be shared and synchronized between persons.\"}]}");
        }
    }
}
