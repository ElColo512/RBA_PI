using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RBA_PI.Infrastructure.Identity.Entities;

namespace RBA_PI.Infrastructure.Identity
{
    public class AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.IdUsuario)
                      .ValueGeneratedOnAdd()
                      .Metadata.SetAfterSaveBehavior(
                          Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
            });
        }
    }
}
