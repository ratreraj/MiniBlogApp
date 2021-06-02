using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniBlogApp.Entities;
using MiniBlogApp.Repositories.Implementions;
using MiniBlogApp.Repositories.Interfaces;
using MiniBlogApp.Services.Implementation;
using MiniBlogApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlogApp.WebUI.Configuration
{
    public static class ConfigurationDependency
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IRepository<Articles>, Repository<Articles>>();
            services.AddScoped<IRepository<Comments>, Repository<Comments>>();
            services.AddScoped<IRepository<Reply>, Repository<Reply>>();
        }
    }
}
