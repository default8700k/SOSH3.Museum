using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace SOSH3.Museum.WebApplication.Tests
{
    public class WebApplicationTestFactory : WebApplicationFactory<Startup>
    {
        public HttpClient HttpClient { get; }

        public WebApplicationTestFactory()
        {
            this.HttpClient = base.CreateClient();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
        }
    }
}
