using JhonnySe.Controllers;
using JhonnySe.Models.GitHub;
using JhonnySe.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace JhonnySe.Test
{
    public class HomeControllerTests
    {
        [Fact]
        public async Task ModelOwnerNameShouldBeSet()
        {
            //Arrange
            var (mockUser, mockRepoList) = GetMockDataObjects();
            
            var mockGithubRepo = new Mock<IGitHubRepository>();
            mockGithubRepo.Setup(u => u.GetUser("JhonnyLi")).Returns(Task.FromResult(mockUser));
            mockGithubRepo.Setup(r => r.GetReposFromUser(mockUser)).Returns(Task.FromResult(mockRepoList));

            var mockLinkedInRepo = new Mock<ILinkedinRepository>();
            mockLinkedInRepo.Setup(l => l.GetLinkedInProfileLink()).Returns("");

            var controller = new HomeController(mockGithubRepo.Object, mockLinkedInRepo.Object);

            //Act
            var result = await controller.Index();

            //Assert
            var model = (MainViewModel)((ViewResult)result).Model;
            Assert.Equal("JhonnyLi", model.OwnerName);
        }

        private (GitHubUser user, List<Repository> repositories) GetMockDataObjects()
        {
            var mockUser = new GitHubUser
            {
                name = "JhonnyLi",
                avatar_url = "",
                html_url = ""
            };
            var mockRepoList = new List<Repository>()
            {
                new Repository
                {
                    name = "",
                    created_at = DateTime.Now,
                    description = "",
                    updated_at = DateTime.Now
                }
            };

            return (mockUser, mockRepoList);
        }
    }
}
