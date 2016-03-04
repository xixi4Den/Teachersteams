using System.Collections.Generic;
using System.Web.Http;
using Teachersteams.Business.Services;

namespace Teachersteams.Business.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly ITestService testService;

        public ValuesController(ITestService testService)
        {
            this.testService = testService;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            var secondVal = testService.AddTwo(5);
            return new string[] { "value1 = 2", "value2 = " + secondVal };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
