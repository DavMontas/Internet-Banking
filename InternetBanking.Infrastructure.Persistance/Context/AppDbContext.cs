using InternetBanking.Core.Domain.Common;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InternetBanking.Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        #region dbSets -->

        public DbSet<User> User { get; set; }



        #endregion

        public override Task<int> SaveChangesAsync(CancellationToken ct = new())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBE>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";

                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(ct);
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            #region tables

            mb.Entity<User>()
                .ToTable("User");

            #endregion

            #region primary keys
            mb.Entity<User>()
            .HasKey(e => e.Id);
            #endregion

            #region relations
            //relation example
            //mb.Entity<User>()
            //  .HasMany<Advertising>(e => e.Ads)
            //  .WithOne(e => e.User)
            //  .HasForeignKey(e => e.UserId)
            //  .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region property configurations

            #region 'Users'
            mb.Entity<User>()
                .Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(100);

            mb.Entity<User>()
                .Property(e => e.FirstName)
                .IsRequired();

            mb.Entity<User>()
                .Property(e => e.Password)
                .IsRequired();

            mb.Entity<User>()
                .Property(e => e.Email)
                .IsRequired();

            mb.Entity<User>()
                .Property(e => e.PhoneNumber)
                .IsRequired();

            #endregion

            #endregion
        }
    }
}
