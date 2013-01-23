using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBlog.Controllers
{
    public class MongoController : Controller
    {
        public MongoDatabase Database { get; set; }

    }
}
