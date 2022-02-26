using AutoFixture.Xunit2;
using FluentAssertions.AspNetCore.Mvc;
using SOSH3.Museum.WebApplication.Controllers;
using Xunit;

namespace SOSH3.Museum.WebApplication.Tests.Controllers
{
    public class HomeControllerTests
    {
        private readonly HomeController target;

        public HomeControllerTests()
        {
            this.target = new HomeController();
        }

        [Theory]
        [AutoData]
        public void Index_ShouldBeViewResult(string page)
        {
            // setup
            // nothing

            // act
            var result = target.Index(page);

            // assert
            result.Should().BeViewResult();
        }
    }
}
