using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestAngular.Api
{
    public class UserController : ApiController
    {
        public HttpStatusCode Post(UserModel user)
        {
            throw new Exception("Test Exception");
            return HttpStatusCode.Accepted;
        }

        public HttpStatusCode Put(UserModel user)
        {
            return HttpStatusCode.Accepted;
        }
    }

    public class UserModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
    }
}
