using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ProjectManagement.Api.Controllers
{
    public abstract class BaseController<T> : ControllerBase
    {
        [HttpGet]
        public IEnumerable<T> Get()
        {
            return GetInformation();
        }

        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long id)
        {
            var data = GetInformationOnId(id);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest("no data found for the ID");
        }

        [HttpPost]
        [Route("Update")]
        public  IActionResult Post(T data)
        {
            return UpdateInformation(data);
        }

        [HttpPut]
        [Route("Add")]
        public IActionResult Put(T data)
        {
            return AddInformation(data);
        }

        [HttpDelete("{id:long}")]
        public IActionResult Delete(long id)
        {
            if(DeleteInformation(id))
            {
                return Ok();
            }
            return BadRequest();
        }

        protected virtual IActionResult UpdateInformation(T data)
        {
            throw new NotImplementedException();
        }

        protected virtual bool DeleteInformation(long id)
        {
            throw new NotImplementedException();
        }
        protected virtual IActionResult AddInformation(T Data)
        {
            throw new NotImplementedException();
        }

        protected virtual T GetInformationOnId(long id)
        {
            throw new NotImplementedException();
        }

        protected virtual IEnumerable<T> GetInformation()
        {
            throw new NotImplementedException();
        }
    }
}
