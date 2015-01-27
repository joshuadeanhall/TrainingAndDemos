using Statuos.Domain;
using Statuos.Web.Areas.Admin.Models;
using System.Collections.Generic;
using Statuos.Web.Infrastructure.AutoMapper;
using Statuos.Web.Models;
using Castle.Windsor;

namespace Statuos.Web.Infrastructure.Helpers
{
    public static class TypeHelper
    {
        public static List<TypeViewModel> GetTypes<T>() where T : class
        {
            List<TypeViewModel> projectTypes = new List<TypeViewModel>();
            if (MvcApplication._container == null)
                return new List<TypeViewModel>();
            //TODO this is hard to test.
            //TODO Should things be manually released?
            var projects = MvcApplication._container.ResolveAll<T>();

            foreach (var project in projects)
            {
                projectTypes.Add(new TypeViewModel() { Type = project.GetType().ToString(), Name = project.GetType().Name });
            }
            return projectTypes;
        }
    }
}