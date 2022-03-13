using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZM_CS296N_TermProject.Models.DomainModels;

namespace ZM_CS296N_TermProject.Models.DataLayer
{
    public class GameSiteContext : IdentityDbContext<AppUser>
    {
        public GameSiteContext(DbContextOptions<GameSiteContext> options) : base(options) { }
        public GameSiteContext() { }
        public DbSet<Review> reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
   
