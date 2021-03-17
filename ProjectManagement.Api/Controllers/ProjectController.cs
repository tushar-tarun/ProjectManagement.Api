using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entities;
using ProjectManagement.Shared;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/Project")]
    public class ProjectController : BaseController<Project>
    {
        private ProjectManagementDbContext _context;
        public ProjectController(ProjectManagementDbContext projectContext)
        {
            _context = projectContext;
        }


        protected override Project GetInformationOnId(long id)
        {
            var data = _context.Projects.Find(id);
            if(data != null)
            {
                return data;
            }
            return null;
        }

        protected override IEnumerable<Project> GetInformation()
        {
            return _context.Projects.ToList(); 
        }

        protected override IActionResult UpdateInformation(Project project)
        {
            try
            {
                _context.Projects.Update(project);
                _context.SaveChanges();
                return Ok(project);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Record does not exist in the database");
            }
        }

        protected override bool DeleteInformation(long id)
        {
            var itemToDelete = _context.Projects.Find(id);
            if(itemToDelete != null)
            {
                _context.Projects.Remove(itemToDelete);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        protected override IActionResult AddInformation(Project project)
        {
            var currentProject = _context.Projects.Find(project.ID);
            if (currentProject == null)
            {
                _context.Projects.Add(project);
                _context.SaveChanges();
                return Ok(project);
            }

            return BadRequest("Entry for the Project ID already exists");
        }
    }
}
