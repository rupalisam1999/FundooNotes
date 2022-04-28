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
        public DbSet<Entity.Note> Notes { get; set; }
        public DbSet<Entity.Lable> Lables { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Entity.User>()
            .HasIndex(u => u.Email)
            .IsUnique();

            modelBuilder.Entity<Entity.Lable>()
               .HasKey(un => new { un.UserId, un.NoteId });

            modelBuilder.Entity<Entity.Lable>()
                .HasOne<Entity.User>(u => u.User)
                 .WithMany(s => s.lables)
                 .HasForeignKey(u => u.UserId)
                 .OnDelete(DeleteBehavior.Cascade); //Cascade behaviour


            modelBuilder.Entity<Entity.Lable>()
               .HasOne<Entity.Note>(u => u.Note)
                .WithMany(s => s.lables)
                .HasForeignKey(u => u.NoteId)
                .OnDelete(DeleteBehavior.Cascade); //Cascade behaviour
        }
    }
}
