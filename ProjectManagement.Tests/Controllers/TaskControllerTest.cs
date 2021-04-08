using Microsoft.AspNetCore.Mvc.Testing;
using ProjectManagement.Api;
using ProjectManagement.Entities;
using ProjectManagement.Entities.Enums;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProjectManagement.Tests
{
    public class TaskControllerTest : ProjectManagememtControllerTestBase<Task>
    {
        public TaskControllerTest(WebApplicationFactory<Startup> webApplicationFactory) 
            : base(webApplicationFactory)
        {
        }

        [Fact]
        public void TestProjectControllerMethods()
        {
            TestControllerMethods("Task");
        }

        protected override void AssertGetAllInformation(List<Task> tasks)
        {
            Assert.NotNull(tasks);
            Assert.Equal(3, tasks.Count);

            var testData = tasks.FirstOrDefault(x => x.ID == 1);
            AssertGetOneInformation(testData);
        }

        protected override void AssertGetOneInformation(Task task)
        {
            Assert.NotNull(task);
            Assert.Equal(TaskStatus.InProgress, task.Status);
            Assert.Equal(1, task.AssignedToUserID);
            Assert.Equal("This is a test task", task.Detail);
        }

        protected override void AssertUpdatedOneInformation(Task task)
        {
            Assert.NotNull(task);
            Assert.Equal(TaskStatus.Completed, task.Status);
        }

        protected override Task GetDataToAdd()
        {
            return new Task
            {
                ID = 5,
                AssignedToUserID = 1,
                Status = TaskStatus.New,
                Detail = "Unit test task"
            };
        }

        protected override Task GetDataToUpdate(Task task)
        {
            task.Status = TaskStatus.Completed;
            return task;
        }
    }
}
