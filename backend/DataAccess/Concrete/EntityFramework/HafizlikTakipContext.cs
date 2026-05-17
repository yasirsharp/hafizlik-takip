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

        private int CurrentTenantId => Core.Utilities.Tenant.TenantResolver.GetCurrentTenantId();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Global Query Filters (TenantId = 0 means system/admin, sees all)
            modelBuilder.Entity<Student>().HasQueryFilter(e => CurrentTenantId == 0 || e.TenantId == CurrentTenantId);
            modelBuilder.Entity<Class>().HasQueryFilter(e => CurrentTenantId == 0 || e.TenantId == CurrentTenantId);
            modelBuilder.Entity<Lesson>().HasQueryFilter(e => CurrentTenantId == 0 || e.TenantId == CurrentTenantId);
            modelBuilder.Entity<Mistake>().HasQueryFilter(e => CurrentTenantId == 0 || e.TenantId == CurrentTenantId);
            modelBuilder.Entity<Homework>().HasQueryFilter(e => CurrentTenantId == 0 || e.TenantId == CurrentTenantId);
            modelBuilder.Entity<EtutNote>().HasQueryFilter(e => CurrentTenantId == 0 || e.TenantId == CurrentTenantId);
            modelBuilder.Entity<Attendance>().HasQueryFilter(e => CurrentTenantId == 0 || e.TenantId == CurrentTenantId);
            modelBuilder.Entity<VideoRecord>().HasQueryFilter(e => CurrentTenantId == 0 || e.TenantId == CurrentTenantId);
            modelBuilder.Entity<DutyScheduleConstraint>().HasQueryFilter(e => CurrentTenantId == 0 || e.TenantId == CurrentTenantId);
            modelBuilder.Entity<DutyScheduleWeek>().HasQueryFilter(e => CurrentTenantId == 0 || e.TenantId == CurrentTenantId);
            modelBuilder.Entity<TeacherStudentAssignment>().HasQueryFilter(e => CurrentTenantId == 0 || e.TenantId == CurrentTenantId);
        }

        public override int SaveChanges()
        {
            SetTenantIdOnAddedEntities();
            return base.SaveChanges();
        }

        private void SetTenantIdOnAddedEntities()
        {
            var tenantId = CurrentTenantId;
            if (tenantId > 0)
            {
                var addedEntities = ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added && e.Entity is Core.Entities.IHasTenant);

                foreach (var entry in addedEntities)
                {
                    var entity = (Core.Entities.IHasTenant)entry.Entity;
                    if (entity.TenantId == 0) // Eger manuel set edilmediyse, gecerli tenanti ata
                    {
                        entity.TenantId = tenantId;
                    }
                }
            }
        }
    }
}
