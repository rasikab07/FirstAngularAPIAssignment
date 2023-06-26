using System;
using System.Linq;
using AngularApiAssignment1.Models.Entities;
using AngularApiAssignment1.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AngularApiAssignment1.Data
{
    public class ManageEmployeesContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public ManageEmployeesContext(DbContextOptions options) : base(options)
        {

        }

        public ManageEmployeesContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Employee>().Property(s => s.Id).IsRequired();
            modelBuilder.Entity<Employee>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<Employee>().Property(s => s.ContactNumber);
            modelBuilder.Entity<Employee>().Property(s => s.Email);
            modelBuilder.Entity<Employee>().Property(s => s.Gender);

            modelBuilder.Entity<Employee>().HasMany(s => s.employeeSkills).WithOne(s => s.Employee).HasForeignKey(d=>d.EmployeeId).OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<Skill>().ToTable("Skill");
            modelBuilder.Entity<Skill>().Property(s => s.SkillName).IsRequired();
            modelBuilder.Entity<Skill>().Property(s => s.SkillName).IsRequired();
        }
    }
}
