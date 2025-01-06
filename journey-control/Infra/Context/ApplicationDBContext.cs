using journey_control.Models;
using Microsoft.EntityFrameworkCore;

namespace journey_control.Infra.Context
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Models.Version> Versions { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Entrie> Entries { get; set; }
        public DbSet<LocalEntrie> LocalEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=jcDB;Username=root;Password=root");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Versions)
                .WithOne(v => v.Project)
                .HasForeignKey(v => v.ProjectId);

            modelBuilder.Entity<Models.Version>()
                .HasKey(v => new { v.Id, v.ProjectId });

            modelBuilder.Entity<Models.Version>()
                .HasMany(v => v.Tasks)
                .WithOne(t => t.Version)
                .HasForeignKey(t => new { t.VersionId, t.VersionProjectId });

            modelBuilder.Entity<Models.Task>()
                .HasKey(t => new { t.Id, t.UserId });

            modelBuilder.Entity<Models.Task>()
                .HasMany(t => t.Entries)
                .WithOne(e => e.Task)
                .HasForeignKey(e => new { e.TaskId, e.TaskUserId });

            modelBuilder.Entity<Models.Task>()
                .HasMany(t => t.LocalEntries)
                .WithOne(le => le.Task)
                .HasForeignKey(le => new { le.TaskId, le.TaskUserId });

            base.OnModelCreating(modelBuilder);
        }

    }
}
