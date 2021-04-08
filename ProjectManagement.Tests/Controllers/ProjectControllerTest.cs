using Microsoft.AspNetCore.Mvc.Testing;
using ProjectManagement.Api;
using ProjectManagement.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProjectManagement.Tests
{
    public class ProjectControllerTest : ProjectManagememtControllerTestBase<Project>
    {
        public ProjectControllerTest(WebApplicationFactory<Startup> webApplicationFactory) 
            : base(webApplicationFactory)
        {
        }

        [Fact]
        public void TestProjectControllerMethods()
        {
            TestControllerMethods("Project");
        }

        protected override void AssertGetAllInformation(List<Project> projects)
        {
            Assert.NotNull(projects);
            Assert.Equal(4, projects.Count);

            var testProject = projects.FirstOrDefault(project => project.ID == 1);
            AssertGetOneInformation(testProject);
        }

        protected override void AssertGetOneInformation(Project testProject)
        {
            Assert.NotNull(testProject);
            Assert.Equal("TestProject1", testProject.Name);
            Assert.Equal("This is a test project", testProject.Detail);
            Assert.Empty(testProject.Tasks);
        }

        protected override Project GetDataToAdd()
        {
            return new Project
            {
                ID = 5,
                Detail = "Project insert through UnitTest",
                Name = "Project5"
            };
        }
        protected override Project GetDataToUpdate(Project project)
        {
            project.Name = "UpdatedProject5";
            return project;
        }

        protected override void AssertUpdatedOneInformation(Project testProject)
        {
            Assert.NotNull(testProject);
            Assert.Equal("UpdatedProject5", testProject.Name);
        }
    }
}
