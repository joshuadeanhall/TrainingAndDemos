using Statuos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statuos.Data.Queries
{
    public static class UserIsProjectManagerQueryExtension
    {
        public static IQueryable<Project> UserIsProjectManagerQuery(this IQueryable<Project> projects,string userName)
        {
            return projects.Where(p => p.ProjectManager.UserName == userName && p.ProjectManager.IsActive);            
        }
    }
}
