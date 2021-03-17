using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entities;
using ProjectManagement.Shared;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : BaseController<User>
    {
        private ProjectManagementDbContext _context;
        public UserController(ProjectManagementDbContext projectContext)
        {
            _context = projectContext;
        }

        protected override User GetInformationOnId(long id)
        {
            var data = _context.Users.Find(id);
            if (data != null)
            {
                return data;
            }
            return null;
        }

        protected override IEnumerable<User> GetInformation()
        {
            return _context.Users.ToList();
        }

        protected override IActionResult UpdateInformation(User user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                return Ok(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Record does not exist in the database");
            }
        }

        protected override bool DeleteInformation(long id)
        {
            var itemToDelete = _context.Users.Find(id);
            if (itemToDelete != null)
            {
                _context.Users.Remove(itemToDelete);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        protected override IActionResult AddInformation(User user)
        {
            var currentTask = _context.Users.Find(user.ID);
            if (currentTask == null)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok(user);
            }

            return BadRequest("Entry for the Task ID already exists");
        }
    }
}