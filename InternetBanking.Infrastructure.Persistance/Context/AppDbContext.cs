﻿using InternetBanking.Core.Domain.Common;
using InternetBanking.Core.Domain.Entities;
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
        public DbSet<Product> Products { get; set; }
        public DbSet<TypeAccount> TypeAccounts { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
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
            mb.Entity<Product>()
                .ToTable("Products");

            mb.Entity<TypeAccount>()
                .ToTable("TypeAccount");

            mb.Entity<Recipient>()
                .ToTable("Recipient");
            #endregion

            #region primary keys

            mb.Entity<Product>()
                .HasKey(e => e.Id);

            mb.Entity<TypeAccount>()
                .HasKey(e => e.Id);

            mb.Entity<Recipient>()
                .HasKey(e => e.Id);

            #endregion

            #region relations

            mb.Entity<TypeAccount>()
                .HasMany<Product>(t => t.Products)
                .WithOne(p => p.TypeAccount)
                .HasForeignKey(p => p.TypeAccountId)
                .OnDelete(DeleteBehavior.Cascade);

            //mb.Entity<Recipient>()
            //    .HasOne<Product>(r => r.Product)
            //    .WithMany(p => p.Re)
            #endregion

            #region property configurations

            #region products
            //mb.Entity<Product>()
            //    .Property(p => p.Id)
            //    .IsRequired();
            #endregion

            #region typeaccount

            mb.Entity<TypeAccount>()
                .Property(e => e.NameAccount)
                .IsRequired()
                .HasMaxLength(150);

            #endregion

            #endregion
        }
    }
}
