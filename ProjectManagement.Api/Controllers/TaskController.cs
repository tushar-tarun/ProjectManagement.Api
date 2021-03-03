using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Api.Model;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/Task")]
    public class TaskController : BaseController<Task>
    {
        public TaskController()
        {
            baseInformation = InitialData.tasks;
        }

        protected override Task GetInformationOnId(long id)
        {
            return baseInformation.FirstOrDefault(x => x.ID == id);
        }
        protected override IEnumerable<Task> GetInformation()
        {
            return baseInformation.ToArray();
        }
        [HttpDelete]
        public IActionResult DeleteById(long id)
        {
            if (DeleteInformation(id))
            {
                return Ok();
            }
            return BadRequest();
        }
        protected override IActionResult UpdateInformation(Task data)
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
        protected override IActionResult AddInformation(Task Data)
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
