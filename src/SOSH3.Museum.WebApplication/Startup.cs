using Microsoft.Extensions.WebEncoders;
using SOSH3.Museum.WebApplication.Middlewares;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace SOSH3.Museum.WebApplication
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
            services.Configure<WebEncoderOptions>(options => { options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All); });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() == true) { app.UseDeveloperExceptionPage(); }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseRouting();
            app.UseStaticFiles();

            app.UseMiddleware<RequestMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{page=index}",
                    defaults: new { controller = "Home", action = "Index" }
                );
            });
        }
    }
}
