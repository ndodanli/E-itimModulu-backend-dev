using System;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace DataAccess
{
    public class DataContext : DbContext, IDataContext
    {
        public IAuditHelper AuditHelper { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.HasPostgresExtension("citext");
            // modelBuilder.HasCollation("em-ci-collation", locale: "en-u-ks-primary", provider: "icu", deterministic: false);
            BuildModel.SetRelationships(modelBuilder);
        }

        public DataContext(IAuditHelper auditHelper, DbContextOptions options) : base(options)
        {
            AuditHelper = auditHelper;
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is TimeFieldsWithDeclaration)
                {
                    var now = DateTime.Now;
                    var userName = (entry.Entity is RefreshToken) ? "" : AuditHelper.GetCurrentUser();
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.CurrentValues["UpdatedAt"] = now;
                            entry.CurrentValues["UpdatedBy"] = userName;
                            break;

                        case EntityState.Added:
                            entry.CurrentValues["CreatedAt"] = now;
                            entry.CurrentValues["UpdatedAt"] = now;
                            if (!(entry.Entity is RefreshToken))
                                entry.CurrentValues["UpdatedBy"] = userName;
                            break;
                    }
                }
                else if (entry.Entity is TimeFieldsWithoutDeclaration)
                {
                    var now = DateTime.Now;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.CurrentValues["UpdatedAt"] = now;
                            break;

                        case EntityState.Added:
                            entry.CurrentValues["CreatedAt"] = now;
                            entry.CurrentValues["UpdatedAt"] = now;
                            break;
                    }
                }
            }
        }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<MediaFile> MediaFiles { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
