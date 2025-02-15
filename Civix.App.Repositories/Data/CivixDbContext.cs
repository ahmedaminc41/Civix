using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Civix.App.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Civix.App.Repositories.Data
{
    public class CivixDbContext : IdentityDbContext<AppUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CivixDbContext(DbContextOptions<CivixDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var entries = ChangeTracker.Entries<AuditableEntity>();

            foreach (var item in entries)
            {

                var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);


                if (item.State == EntityState.Added)
                {
                    item.Property(X => X.CreatedById).CurrentValue = currentUserId;
                }
                else if (item.State == EntityState.Modified)
                {
                    item.Property(X => X.UpdatedById).CurrentValue = currentUserId;
                    item.Property(X => X.UpdatedOn).CurrentValue = DateTime.UtcNow;
                }

            }


            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<OtpRecord> OtpRecords { get; set; }

        public DbSet<Issue> Issues { get; set; }
        public DbSet<Category> Categories { get; set; }


    }
}
