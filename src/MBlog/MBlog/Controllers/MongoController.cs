using Castle.Core.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBlog.Controllers
{
    public abstract class MongoController : Controller
    {
        public ILogger Logger { get; set; }
        public MongoDatabase Database { get; set; }

    }
}
