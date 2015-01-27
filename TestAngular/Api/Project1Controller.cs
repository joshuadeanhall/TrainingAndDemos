using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestAngular.Api
{
    public class Project1Controller : ApiController
    {
        [Route("api/v1/projects")]
        public IEnumerable<string> Get()
        {
            return new List<string>
            {
                "Test",
                "Test2"
            };
        }
    }
}
