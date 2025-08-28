using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using konsume_v1.Core.Domain.Entities;
namespace konsume_v1.Infrastructure.Context
{
    public class KonsumeContext : DbContext
    {
        public KonsumeContext(DbContextOptions<KonsumeContext> opt) : base(opt)
        {

        }

        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Profile> Profiles => Set<Profile>();
        public DbSet<VerificationCode> VerificationCodes => Set<VerificationCode>();

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                            v => v.ToUniversalTime(),
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                    }
                }
            }

            var concernsConverter = new ValueConverter<List<string>, string>(
       v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
       v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
   );

            var concernsComparer = new ValueComparer<List<string>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()
            );


            modelBuilder.Entity<Role>().Property<int>("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property<int>("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Profile>().Property<int>("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<VerificationCode>().Property<int>("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<VerificationCode>()
            .HasOne(vc => vc.User)
            .WithMany(u => u.VerificationCodes)
            .HasForeignKey(vc => vc.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Profile>()
            .Property(p => p.AllergiesSerialized)
            .HasConversion(
                v => v,
                v => v
            );

            modelBuilder.Entity<Profile>()
            .Property(p => p.GoalsSerialized)
            .HasConversion(
                v => v,
                v => v
            );

            modelBuilder.Entity<Profile>().Ignore(p => p.Allergies);
            modelBuilder.Entity<Profile>().Ignore(p => p.UserGoals);

            modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, DateCreated = DateTime.UtcNow, Name = "Admin", CreatedBy = "1" },
            new Role { Id = 2, DateCreated = DateTime.UtcNow, Name = "Patient", CreatedBy = "1" },
             new Role { Id = 3, DateCreated = DateTime.UtcNow, Name = "restaurant", CreatedBy = "1" },
              new Role { Id = 4, DateCreated = DateTime.UtcNow, Name = "company", CreatedBy = "1" }
            );

            modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                DateCreated = DateTime.UtcNow,
                FirstName = "Hasbiy",
                LastName = "Oyebo",
                IsDeleted = false,
                Email = "oyebohm@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("hasbiyallah"),
                RoleId = 1,
                CreatedBy = "ManualRegistration",
            });

            modelBuilder.Entity<Profile>().HasData(
            new Profile
            {
                Id = 1,
                Weight = 45,
                Gender = Core.Domain.Enums.Gender.Female,
                DateOfBirth = DateTime.SpecifyKind(new DateTime(2008, 3, 19, 0, 0, 0), DateTimeKind.Utc),
                DateCreated = DateTime.UtcNow,
                Height = 90,
                IsDeleted = false,
                UserId = 1,
                CreatedBy = "1",
                Nationality = "Nigerian",
                SkinType = "normal skin",
                AllergiesSerialized = JsonSerializer.Serialize(new List<string>()),
                GoalsSerialized = JsonSerializer.Serialize(new List<string>())
            });
        }
    }
}
