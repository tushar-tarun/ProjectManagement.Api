using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Model;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : BaseController<User>
    {

        public UserController() 
        {
            baseInformation = InitialData.users;
        }

        protected override User GetInformationOnId(long id)
        {
            return baseInformation.FirstOrDefault(x => x.ID == id);
        }

        protected override IEnumerable<User> GetInformation()
        {
            IList<User> users = new List<User>();

            foreach(var user in baseInformation)
            {
                users.Add(new User { Email = user.Email, FirstName = user.FirstName, ID = user.ID, LastName = user.LastName, Tasks = InitialData.tasks.Where(task => task.AssignedToUserID == user.ID) });
            }

            if (users.Count == 0)
            {
                return null;
            }

            return users;
        }

        protected override IActionResult UpdateInformation(User data)
        {
            var dataToUpdate = baseInformation.FirstOrDefault(x => x.ID == data.ID);
            if (dataToUpdate == null)
            {
                return AddInformation(data);
            }
            else
            {
                if (DeleteInformation(data.ID))
                {
                    return AddInformation(data);
                }
                return BadRequest();
            }
        }

        protected override bool DeleteInformation(long id)
        {
            var dataToDelete = baseInformation.FirstOrDefault(x => x.ID == id);
            if (dataToDelete == null)
            {
                return false;
            }
            else
            {
                baseInformation.Remove(dataToDelete);
                return true;
            }
        }
        protected override IActionResult AddInformation(User Data)
        {
            try
            {
                baseInformation.Add(Data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(500);
            }
        }
    }
}
