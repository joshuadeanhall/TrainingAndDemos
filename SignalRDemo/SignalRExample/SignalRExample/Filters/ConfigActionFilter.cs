using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Microsoft.WindowsAzure;

namespace SignalRExample.Filters
{
    public class ConfigActionFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.SignalRUrl = CloudConfigurationManager.GetSetting("SignalRUrl");
            base.OnResultExecuting(filterContext);
        }
    }
}