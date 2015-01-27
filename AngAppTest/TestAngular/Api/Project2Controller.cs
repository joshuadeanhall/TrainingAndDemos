using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestAngular.Api
{
    public class Project2Controller : ApiController
    {
        [Route("api/v2/projects")]
        public IEnumerable<string> Get()
        {
            return new List<string>
            {
                "Testv2",
                "Testv22"
            };
        }

        [Route("api/v2/savespecialproject")]
        public string PostSpecialSave([FromBody]int id)
        {
            var spcialid = id*id;
            return spcialid.ToString();
        }

        [Route("api/v2/projects")]
        public string Post([FromBody]int id)
        {
            return id.ToString();
        }
    }
}
