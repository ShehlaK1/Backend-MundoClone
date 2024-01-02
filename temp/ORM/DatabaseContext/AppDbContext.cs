using Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ORM.DatabaseContext
{
    public class AppDbContext : DbContext
    {
        private IConfigurationRoot _configuration { get; set; }
        public AppDbContext()
        {
            InitConfigurations();
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            InitConfigurations();
        }

        private void InitConfigurations()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
            .Build();
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder
            .Entity<Year>()
            .Property(e => e.StartDate)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Local));

            modelBuilder
            .Entity<Year>()
            .Property(e => e.EndDate)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Local));

            //modelBuilder.Entity<UserProfile>().HasData(new UserProfile
            //{
            //	Id = "7d0f3035-8216-4537-971e-734f05ad7dc1",
            //	Name = "Some Name",
            //	Department = "Department Comes Here",
            //	Description = "This is some description",
            //	Designation = "This is the Designation",
            //});

            //modelBuilder
            //.Entity<User>()
            //.Property(e => e.Role)
            //.HasConversion(
            //    v => v.ToString(),
            //    v => (RoleSelection)Enum.Parse(typeof(RoleSelection), v));

            modelBuilder.Entity<Assessment>()
                .HasOne(a => a.TargetSkill)
                .WithMany()
                .HasForeignKey(a => a.TargetSkillId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.TargetSkill)
                .WithMany()
                .HasForeignKey(l => l.TargetSkillId)
                .OnDelete(DeleteBehavior.Restrict); 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_configuration != null)
            {
                string connectionString = _configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TargetSkill> TargetSkills { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Year> Years { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<CompositeLevel> CompositeLevels { get; set; }
        public virtual DbSet<Assessment> Assessments { get; set; }






    }
}
