using System.Web.Mvc;

namespace Statuos.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "Admin_create_project_type",
            //    "Admin/Project/Create/{ProjectType}",
            //    new { controller = "Project", ProjectType = UrlParameter.Optional, action = "Create" },
            //    new[] { "Statuos.Web.Areas.Admin.Controllers" }
            //    );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Statuos.Web.Areas.Admin.Controllers" }
            );
        }
    }
}
