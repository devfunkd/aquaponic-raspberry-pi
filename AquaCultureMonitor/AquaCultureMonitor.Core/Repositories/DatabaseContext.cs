using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using AquaCultureMonitor.Core.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AquaCultureMonitor.Core.Repositories
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>, IDatabaseContext
    {
        public DatabaseContext() 
            : base("name=DatabaseContext")
        {
            Database.SetInitializer<DatabaseContext>(null);
        }

        public IDbSet<Sensor> Sensors { get; set; }
        public IDbSet<DataReading> SensorData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sensor>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<DataReading>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DataReading>()
                .ToTable("Data");

            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users");

            modelBuilder.Entity<ApplicationUser>()
                .ToTable("Users");

            modelBuilder.Entity<IdentityUser>()
                        .HasMany(u => u.Roles)
                        .WithOptional()
                        .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<IdentityUser>()
                        .HasMany(u => u.Logins)
                        .WithOptional()
                        .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<IdentityUser>()
                        .HasMany(u => u.Claims)
                        .WithOptional()
                        .HasForeignKey(c => c.UserId);
        }

        public static DatabaseContext Create()
        {
            return new DatabaseContext();
        }
    }
}

