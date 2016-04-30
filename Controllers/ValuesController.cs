using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Model.Data;
using Model.Domain;

namespace balance_api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IEventMappingDao eventMappingDao;
        
        public ValuesController(IEventMappingDao eventMappingDao)
        {
            this.eventMappingDao = eventMappingDao;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //            List<EventMapping> mappings = eventMappingDao.findAll();
            //            return new string[] { "value1",  mappings.Count.ToString() };
            return new string[] { "asdf", "asdf" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        
        // GET api/values
        //[HttpGet]
        public List<EventMapping> GetEventMappings() {
            return eventMappingDao.findAll();
        }
    }
}
