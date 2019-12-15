using JhonnySe.Repositorys;
using JhonnySe.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JhonnySe
{
    public class Startup
    {
        public IWebHostEnvironment HostingEnvironment { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.HostingEnvironment = env;
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddControllersWithViews();

            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddSingleton<ISecretsRepository, SecretsRepository>();
            services.AddScoped<IGitHubRepository, GitHubRepository>();
            services.AddScoped<ILinkedinRepository, LinkedinRepository>();
            services.AddSingleton<IBlobStorageClient, BlobStorageClient>();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
