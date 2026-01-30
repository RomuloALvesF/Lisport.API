using Lisport.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Lisport.API.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<ClassGroup> ClassGroups { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<AttendanceSession> AttendanceSessions { get; set; } = null!;
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; } = null!;
        public DbSet<StudentEvolution> StudentEvolutions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Role).HasConversion<string>();
            });

            modelBuilder.Entity<ClassGroup>(entity =>
            {
                entity.Property(c => c.Name).IsRequired();
                entity.Property(c => c.Modality).IsRequired();
                entity.Property(c => c.AgeRange).IsRequired();
                entity.Property(c => c.DaysAndTimes).IsRequired();

                entity.HasOne(c => c.Professor)
                    .WithMany(u => u.Classes)
                    .HasForeignKey(c => c.ProfessorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(s => s.Name).IsRequired();
                entity.Property(s => s.Document).IsRequired();
                entity.Property(s => s.ResponsibleName).IsRequired();
                entity.Property(s => s.Phone).IsRequired();
                entity.Property(s => s.Address).IsRequired();
                entity.Property(s => s.Gender).HasConversion<string>();

                entity.HasOne(s => s.ClassGroup)
                    .WithMany(c => c.Students)
                    .HasForeignKey(s => s.ClassGroupId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AttendanceSession>(entity =>
            {
                entity.Property(a => a.Date).IsRequired();
                entity.HasIndex(a => new { a.ClassGroupId, a.Date }).IsUnique();

                entity.HasOne(a => a.ClassGroup)
                    .WithMany()
                    .HasForeignKey(a => a.ClassGroupId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AttendanceRecord>(entity =>
            {
                entity.Property(a => a.Status).HasConversion<string>();
                entity.HasIndex(a => new { a.AttendanceSessionId, a.StudentId }).IsUnique();

                entity.HasOne(a => a.AttendanceSession)
                    .WithMany(s => s.Records)
                    .HasForeignKey(a => a.AttendanceSessionId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(a => a.Student)
                    .WithMany()
                    .HasForeignKey(a => a.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<StudentEvolution>(entity =>
            {
                entity.Property(e => e.PhysicalScore).IsRequired();
                entity.Property(e => e.TechnicalScore).IsRequired();
                entity.Property(e => e.BehaviorScore).IsRequired();
                entity.HasIndex(e => new { e.StudentId, e.Date }).IsUnique();

                entity.HasOne(e => e.Student)
                    .WithMany()
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
