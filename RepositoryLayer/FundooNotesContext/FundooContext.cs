using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.FundooNotesContext
{
    public class FundooContext : DbContext
    {
        public string Email;

        public FundooContext(DbContextOptions options)
           : base(options)
        {
        }
        public DbSet<Entity.User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Entity.User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        }
    }
}
