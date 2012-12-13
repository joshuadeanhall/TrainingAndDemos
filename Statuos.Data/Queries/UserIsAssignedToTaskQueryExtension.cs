using Statuos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Statuos.Data.Queries
{
    public static class UserIsAssignedToTaskQueryExtension
    {
        public static IQueryable<Task> UserIsAssignedToTaskQuery(this IQueryable<Task> tasks, string userName)
        {
            //User is assigned to task and is an active user or user is project manager of task and is an active user
            return tasks.Where(t => t.Users.Any(u => u.UserName == userName && u.IsActive) || (t.Project.ProjectManager.UserName == userName && t.Project.ProjectManager.IsActive));
        }
    }
}
