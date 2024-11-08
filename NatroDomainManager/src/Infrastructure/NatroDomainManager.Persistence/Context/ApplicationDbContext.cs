using Microsoft.EntityFrameworkCore;
using NatroDomainManager.Application.Interfaces.Context;
using NatroDomainManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NatroDomainManager.Persistence.Context
{
    public class ApplicationDbContext : DbContext, IApplicationContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<InternetName> InternetNames { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Favorite>()
            .HasOne(f => f.InternetName)
            .WithMany()
            .HasForeignKey(f => f.InternetNameId);

            modelBuilder.Entity<Favorite>()
            .HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId);
        }
    }
}
