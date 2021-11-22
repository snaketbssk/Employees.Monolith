using Employees.Monolith.DataLayer.Models.Contexts.Constants;
using Employees.Monolith.DataLayer.Models.Tables;
using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Microsoft.EntityFrameworkCore;
using SnakeTBs.Extensions.Encryptors.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Employees.Monolith.DataLayer.Models.Contexts.Entities
{
    public class DatabaseContext : DbContext
    {
        internal static SettingsContext Settings { get; private set; } = new SettingsContext();
        public DbSet<UserTable> Users { get; set; }
        public DbSet<ReferralTable> Referrals { get; set; }
        public DbSet<TokenTable> Tokens { get; set; }
        public DbSet<PermissionTable> Permissions { get; set; }
        public DbSet<DepartmentTable> Departments { get; set; }
        public DbSet<EmployeeTable> Employees { get; set; }
        public DbSet<SkillTable> Skills { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public static DatabaseContext GetContext()
        {
            var result = new DatabaseContext(Settings.Options);
            return result;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BaseTable.OnModelCreating<UserTable>(modelBuilder);
            modelBuilder.Entity<UserTable>().HasIndex(v => v.Login).IsUnique();
            modelBuilder.Entity<UserTable>().HasIndex(v => v.Email).IsUnique();
            BaseTable.OnModelCreating<TokenTable>(modelBuilder);
            BaseTable.OnModelCreating<ReferralTable>(modelBuilder);
            BaseTable.OnModelCreating<PermissionTable>(modelBuilder);
            BaseTable.OnModelCreating<DepartmentTable>(modelBuilder);
            modelBuilder.Entity<DepartmentTable>().HasIndex(v => v.Title).IsUnique();
            BaseTable.OnModelCreating<EmployeeTable>(modelBuilder);
            modelBuilder.Entity<EmployeeTable>().Property(v => v.Salary).HasColumnType(ContextConstant.DECIMAL);
            BaseTable.OnModelCreating<SkillTable>(modelBuilder);
            modelBuilder.Entity<SkillTable>().HasIndex(v => v.Title).IsUnique();
            SeedData(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            var users = new List<UserTable>();
            users.Add(new UserTable
            {
                Id = users.Count + 1,
                Login = "admin",
                Email = "admin@example.com",
                Password = MD5Encryptor.Encrypt("admin"),
                Role = RoleUserTable.Admin,
                CreatedAt = DateTime.MinValue,
                Guid = Guid.Parse("82a700e7-945d-4bc2-a05a-b5b1fddb423c"),
            });
            var tokens = new List<TokenTable>();
            tokens.Add(new TokenTable
            {
                Id = tokens.Count + 1,
                UserId = users.First(v => v.Login.Equals("admin")).Id,
                IsActive = true,
                CreatedAt = DateTime.MinValue,
                ExpiredAt = DateTime.MaxValue,
                Guid = Guid.Parse("7893d448-7fc5-4fdf-b2f0-7d68a3d190d7"),
            });
            var skills = new List<SkillTable>();
            skills.Add(new SkillTable
            {
                Id = skills.Count + 1,
                Title = "C#",
                CreatedAt = DateTime.MinValue,
                Guid = Guid.Parse("c7ed78b7-b716-4b0c-84fd-9771f54a9d8e")
            });
            skills.Add(new SkillTable
            {
                Id = skills.Count + 1,
                Title = "Python",
                CreatedAt = DateTime.MinValue,
                Guid = Guid.Parse("c7ed78b7-b726-4b0c-84fd-9771f54a9d8e")
            });
            skills.Add(new SkillTable
            {
                Id = skills.Count + 1,
                Title = "Docker",
                CreatedAt = DateTime.MinValue,
                Guid = Guid.Parse("c7ed78b7-b716-4b0c-84fd-9771f54a9d1e")
            });
            var departments = new List<DepartmentTable>();
            departments.Add(new DepartmentTable
            {
                Id = departments.Count + 1,
                Title = "Common",
                CreatedAt = DateTime.MinValue,
                Guid = Guid.Parse("180bd655-7a70-488f-93d1-f0af1fbd12d3")
            });
            var employees = new List<EmployeeTable>();
            //employees.Add(new EmployeeTable
            //{
            //    Id = employees.Count + 1,
            //    FirstName = "Alexander",
            //    LastName = "Simmmons",
            //    DepartmentId = departments.First(v => v.Title.Equals("None")).Id,
            //    Skills = new List<SkillTable>(),
            //    Salary = 1000m,
            //    Guid = Guid.Parse("eaf18aac-fafa-49ea-bbf4-8f75ca9adb28")
            //});
            //
            modelBuilder.Entity<UserTable>().HasData(users);
            modelBuilder.Entity<TokenTable>().HasData(tokens);
            modelBuilder.Entity<DepartmentTable>().HasData(departments);
            modelBuilder.Entity<SkillTable>().HasData(skills);
            // modelBuilder.Entity<EmployeeTable>().HasData(employees);
        }
    }
}
