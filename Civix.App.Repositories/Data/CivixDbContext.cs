using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Civix.App.Repositories.Data
{
    public class CivixDbContext: IdentityDbContext<AppUser>
    {
        public CivixDbContext(DbContextOptions<CivixDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<OtpRecord> OtpRecords { get; set; }

        public DbSet<Issue> Issues { get; set; }
        public DbSet<Category> Categories { get; set; }


    }
}
