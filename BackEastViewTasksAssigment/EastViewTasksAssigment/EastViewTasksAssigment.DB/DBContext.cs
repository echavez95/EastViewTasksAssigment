using EastViewTasksAssignment.DB.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace EastViewTasksAssignment.DB
{
    public class DBContext : DbContext 
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Citizens>(e => e.HasIndex(f => f.citizenIdCard).IsUnique());

            modelBuilder.Entity<Tasks>()
                .HasOne(u => u.Citizen)
                .WithMany(t => t.Tasks)
                .HasForeignKey(u => u.taskCitizenId);
                
        }

        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Citizens> Citizens { get; set; }
    }
}
