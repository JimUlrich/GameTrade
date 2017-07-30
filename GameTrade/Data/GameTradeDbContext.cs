using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GameTrade.Models;

namespace GameTrade.Data
{
    public class GameTradeDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Designation> Designations { get; set; }


        public GameTradeDbContext(DbContextOptions<GameTradeDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
