using JhonnySe.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace JhonnySe.Test
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexTitleShouldBeSet()
        {
            var controller = new HomeController();
            
            var result = controller.Index();
            
            Assert.Equal("Jhonny.se", ((ViewResult)result).ViewData["Title"]);
        }
    }
}
