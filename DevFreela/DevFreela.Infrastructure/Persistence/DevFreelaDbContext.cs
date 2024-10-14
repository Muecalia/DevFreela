using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext : DbContext
    {
        public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : base(options)
        {
            
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<ProjectComment> ProjectComments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserSkill>(e => e.HasKey(uk => uk.Id));

            builder.Entity<Skill>(e => e.HasKey(s => s.Id));

            builder.Entity<ProjectComment>(pc => 
            {
                pc.HasKey(pc => pc.Id);

                pc.HasOne(p => p.Project)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(p => p.IdProject)
                    .OnDelete(DeleteBehavior.Restrict);

                pc.HasOne(pc => pc.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(p => p.IdUser)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<User>(u => 
            {
                u.HasKey(u => u.Id);

                u.HasMany(u => u.Skills)
                    .WithOne()
                    .HasForeignKey(u => u.IdSkill)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Project>(e => 
            {
                e.HasKey(p => p.Id);

                e.HasOne(p => p.Freelancer)
                    .WithMany(f => f.FreelanceProjects)
                    .HasForeignKey(p => p.IdFreelancer)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(p => p.Client)
                    .WithMany(f => f.OwnedProjects)
                    .HasForeignKey(p => p.IdClient)
                    .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}
