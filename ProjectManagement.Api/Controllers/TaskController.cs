using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entities;
using ProjectManagement.Shared;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/Task")]
    public class TaskController : BaseController<Task>
    {
        private ProjectManagementDbContext _context;
        public TaskController(ProjectManagementDbContext projectContext)
        {
            _context = projectContext;
        }

        protected override Task GetInformationOnId(long id)
        {
            var data = _context.Tasks.Find(id);
            if (data != null)
            {
                return data;
            }
            return null;
        }

        protected override IEnumerable<Task> GetInformation()
        {
            return _context.Tasks.ToList();
        }

        protected override IActionResult UpdateInformation(Task task)
        {
            try
            {
                _context.Tasks.Update(task);
                _context.SaveChanges();
                return Ok(task);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Record does not exist in the database");
            }
        }

        protected override bool DeleteInformation(long id)
        {
            var itemToDelete = _context.Tasks.Find(id);
            if (itemToDelete != null)
            {
                _context.Tasks.Remove(itemToDelete);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        protected override IActionResult AddInformation(Task task)
        {
            var currentTask = _context.Tasks.Find(task.ID);
            if (currentTask == null)
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return Ok(task);
            }

            return BadRequest("Entry for the Task ID already exists");
        }
    }
}
