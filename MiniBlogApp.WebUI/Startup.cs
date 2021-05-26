using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiniBlogApp.Services.Configuration;
using MiniBlogApp.WebUI.Configuration;
//using EJ2ScheduleSample.Controllers;

namespace MiniBlogApp.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //if (File.Exists(System.IO.Directory.GetCurrentDirectory() + "/SyncfusionLicense.txt"))
            //{
            //    string licenseKey = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "/SyncfusionLicense.txt");
            //    SyncfusionLicenseProvider.RegisterLicense(licenseKey);
            //}
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigurationRepositries.ConfigureServices(services, Configuration);
            ConfigurationDependency.ConfigureServices(services, Configuration);

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
