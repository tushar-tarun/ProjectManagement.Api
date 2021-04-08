using Microsoft.AspNetCore.Mvc.Testing;
using ProjectManagement.Api;
using ProjectManagement.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProjectManagement.Tests
{
    public class UserControllerTest : ProjectManagememtControllerTestBase<User>
    {
        public UserControllerTest(WebApplicationFactory<Startup> webApplicationFactory) 
            : base(webApplicationFactory)
        {
        }

        [Fact]
        public void TestProjectControllerMethods()
        {
            TestControllerMethods("User");
        }

        protected override void AssertGetAllInformation(List<User> users)
        {
            Assert.NotNull(users);
            Assert.Equal(4, users.Count);

            var testUser = users.FirstOrDefault(x => x.ID == 1);
            AssertGetOneInformation(testUser);
        }

        protected override void AssertGetOneInformation(User user)
        {
            Assert.NotNull(user);
            Assert.Equal("John", user.FirstName);
            Assert.Equal("Doe", user.LastName);
            Assert.Equal("john.doe@test.com", user.Email );
            Assert.Null(user.Tasks);
        }

        protected override void AssertUpdatedOneInformation(User user)
        {
            Assert.NotNull(user);
            Assert.Equal("arun.bhat@update.com", user.Email);
        }

        protected override User GetDataToAdd()
        {
            return new User
            {
                FirstName = "Arun",
                LastName = "Bhat",
                Email = "arun.bhat@test.com",
                ID = 5,
                Password = "password"
            };
        }

        protected override User GetDataToUpdate(User user)
        {
            user.Email = "arun.bhat@update.com";
            return user;
        }
    }
}
