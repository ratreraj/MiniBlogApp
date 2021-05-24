using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniBlogApp.Entities;
using MiniBlogApp.Repositories;

namespace MiniBlogApp.Services.Configuration
{
    public static class ConfigurationRepositries
    {

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(option =>
            {

                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            });

            //services.AddScoped<IRepository, Repository>();
            services.AddIdentity<User, Role>().
                AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddScoped<DbContext, AppDbContext>();

        }

    }
}
