using Core.Utilities.Security.Entities;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class HafizlikTakipContext : DbContext
    {
        // Core entities
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        // Domain entities
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Mistake> Mistakes { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<EtutNote> EtutNotes { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<VideoRecord> VideoRecords { get; set; }
        public DbSet<Surah> Surahs { get; set; }
        public DbSet<Juz> Juzs { get; set; }
        public DbSet<DutyScheduleConstraint> DutyScheduleConstraints { get; set; }
        public DbSet<DutyScheduleWeek> DutyScheduleWeeks { get; set; }
        public DbSet<DutyScheduleEntry> DutyScheduleEntries { get; set; }
        public DbSet<TeacherStudentAssignment> TeacherStudentAssignments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=HafizlikTakipDb;Username=postgres;Password=postgres");
        }
    }
}
