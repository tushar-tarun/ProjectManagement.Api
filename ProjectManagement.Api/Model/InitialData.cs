using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using TaskStatus = ProjectManagement.Entities.Enums.TaskStatus;
using Task = ProjectManagement.Entities.Task;

namespace ProjectManagement.Api.Model
{
    /// <summary>
    /// the class is for the purpose of initial data, till we include Database
    /// </summary>
    public static class InitialData
    {
        public static List<User> users = new List<User>
        {
            new User { ID = 1 , FirstName = "John", LastName = "Doe", Email = "john.doe@test.com", Password = "Password1"},
            new User { ID = 2 , FirstName = "John", LastName = "Skeet", Email = "john.skeet@test.com", Password = "Password1" },
            new User { ID = 3 , FirstName = "Mark", LastName = "Seeman", Email = "mark.seeman@test.com", Password = "Password1" },
            new User { ID = 4 , FirstName = "Bob", LastName = "Martin", Email = "bob.martin@test.com", Password = "Password1" }
        };

        public static List<Task> tasks = new List<Task>
        {
            new Task { ID = 1 , ProjectID = 1, Status = TaskStatus.InProgress, AssignedToUserID = 1, Detail = "This is a test task", CreatedOn = DateTime.Today },
            new Task { ID = 2 , ProjectID = 1, Status = TaskStatus.QA, AssignedToUserID = 2, Detail = "This is a test task", CreatedOn = DateTime.Today },
            new Task { ID = 3 , ProjectID = 2, Status = TaskStatus.Completed, AssignedToUserID = 2, Detail = "This is a test task", CreatedOn = DateTime.Today }
        };

        public static List<Project> projects = new List<Project>
        {
            new Project { ID = 1 ,  Name = "TestProject1", Detail = "This is a test project", CreatedOn = DateTime.Today },
            new Project { ID = 2 ,  Name = "TestProject2", Detail = "This is a test project", CreatedOn = DateTime.Today },
            new Project { ID = 3 ,  Name = "TestProject3", Detail = "This is a test project", CreatedOn = DateTime.Today },
            new Project { ID = 4 ,  Name = "TestProject4", Detail = "This is a test project", CreatedOn = DateTime.Today },
        };
    }
}
