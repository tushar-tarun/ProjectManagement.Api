using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Controllers
{
    public class BaseController<T> : ControllerBase
    {

        protected List<T> baseInformation;

        [HttpGet]
        public IEnumerable<T> Get()
        {
            return GetInformation();
        }

        [HttpGet]
        [Route("{id:long}")]
        public T Get(long id)
        {
            return GetInformationOnId(id);
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
        //[Route("Delete")]
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
