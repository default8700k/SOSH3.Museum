using FluentAssertions;
using Xunit;

namespace SOSH3.Museum.WebApplication.Tests.Controllers
{
    public class HomeControllerIntegrationTests : IClassFixture<WebApplicationTestFactory>
    {
        private readonly WebApplicationTestFactory factory;

        public HomeControllerIntegrationTests(WebApplicationTestFactory factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task Index_ShouldBeCorrect()
        {
            // setup
            // nothing

            // act
            var result = await factory.HttpClient.GetAsync("/");

            // assert
            result.Should().Be200Ok();
            result.Content.Headers.ContentType.Should().BeEquivalentTo(
                new
                {
                    CharSet = "utf-8",
                    MediaType = "text/html"
                }
            );
        }
    }
}
