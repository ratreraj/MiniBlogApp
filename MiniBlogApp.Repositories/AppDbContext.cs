using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniBlogApp.Entities;
using System;

namespace MiniBlogApp.Repositories
{
    public class AppDbContext : IdentityDbContext<User, Role, int>
    {

        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Articles> Articles { get; set; }

        public DbSet<Comments> Comments { get; set; }

        public DbSet<Reply> Replies { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("data source = DESKTOP - HA42MEA\\SQLEXPRESS; initial catalog = MiniBlog; persist security info = True; user id = sa; password = Sql@1234;");
                optionsBuilder.UseSqlServer("data source=KCDESK-GR150; initial catalog=MiniBlog;persist security info=True;user id=sa;password=Sql@2019;");
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
