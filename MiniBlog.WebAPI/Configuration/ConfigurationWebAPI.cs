using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MiniBlogApp.Entities;
using MiniBlogApp.Repositories;
using MiniBlogApp.Repositories.Implementions;
using MiniBlogApp.Repositories.Interfaces;
using MiniBlogApp.Services.Implementation;
using MiniBlogApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.WebAPI.Configuration
{
    public static class ConfigurationWebAPI
    {

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<User, Role>().
                AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddScoped<DbContext, AppDbContext>();

         
            
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IRepository<Articles>, Repository<Articles>>();
            services.AddScoped<IRepository<Comments>, Repository<Comments>>();
            services.AddScoped<IRepository<Reply>, Repository<Reply>>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiniBlog.WebAPI", Version = "v1" });
            });
            services.AddMvc().AddJsonOptions(option =>
            option.JsonSerializerOptions.PropertyNamingPolicy = null);
        }
    }
}
