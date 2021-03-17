using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Entities;
using ProjectManagement.Entities.Enums;
using System;
using System.Linq;

namespace ProjectManagement.Shared
{
    public class ProjectManagementDbContext : DbContext
    {
        public ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        
        public DbSet<Task> Tasks { get; set; }
        
        public DbSet<User> Users { get; set; }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ProjectManagementDbContext(serviceProvider.GetRequiredService<DbContextOptions<ProjectManagementDbContext>>()))
            {
                if (!context.Projects.Any())
                {
                    context.Projects.AddRange(
                        new Project { ID = 1, Name = "TestProject1", Detail = "This is a test project", CreatedOn = DateTime.Today },  
                        new Project { ID = 2, Name = "TestProject2", Detail = "This is a test project", CreatedOn = DateTime.Today },
                        new Project { ID = 3, Name = "TestProject3", Detail = "This is a test project", CreatedOn = DateTime.Today },
                        new Project { ID = 4, Name = "TestProject4", Detail = "This is a test project", CreatedOn = DateTime.Today });
                    context.SaveChanges();
                }

                if (!context.Tasks.Any())
                {
                    context.Tasks.AddRange(new Task { ID = 1, ProjectID = 1, Status = TaskStatus.InProgress, AssignedToUserID = 1, Detail = "This is a test task", CreatedOn = DateTime.Today },
                        new Task { ID = 2, ProjectID = 1, Status = TaskStatus.QA, AssignedToUserID = 2, Detail = "This is a test task", CreatedOn = DateTime.Today },
                        new Task { ID = 3, ProjectID = 2, Status = TaskStatus.Completed, AssignedToUserID = 2, Detail = "This is a test task", CreatedOn = DateTime.Today });
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(new User { ID = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@test.com", Password = "Password1" },
                        new User { ID = 2, FirstName = "John", LastName = "Skeet", Email = "john.skeet@test.com", Password = "Password1" },
                        new User { ID = 3, FirstName = "Mark", LastName = "Seeman", Email = "mark.seeman@test.com", Password = "Password1" },
                        new User { ID = 4, FirstName = "Bob", LastName = "Martin", Email = "bob.martin@test.com", Password = "Password1" });
                    context.SaveChanges();
                }
            }
        }
    }
}
