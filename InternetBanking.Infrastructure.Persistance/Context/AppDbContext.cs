using InternetBanking.Core.Domain.Common;
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
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}

        #region dbSets -->

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

            #endregion

            #region primary keys

            #endregion

            #region relations

            #endregion

            #region property configurations

            #endregion
        }
    }
}
