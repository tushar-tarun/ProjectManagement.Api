using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Model;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/Project")]
    public class ProjectController : BaseController<Project>
    {
        public ProjectController()
        {
            baseInformation = InitialData.projects;
        }

        protected override Project GetInformationOnId(long id)
        {
            return baseInformation.FirstOrDefault(x => x.ID == id);
        }

        protected override IEnumerable<Project> GetInformation()
        {
            return baseInformation.ToArray();
        }

        protected override IActionResult UpdateInformation(Project data)
        {
            var dataToUpdate = baseInformation.FirstOrDefault( x => x.ID == data.ID);
            if(dataToUpdate == null)
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
        protected override IActionResult AddInformation(Project Data)
        {
            try
            {
                baseInformation.Add(Data);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(500);
            }
        }
    }
}
