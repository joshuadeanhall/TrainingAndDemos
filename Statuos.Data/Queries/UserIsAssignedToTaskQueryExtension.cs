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
            return tasks.Where(t => t.Users.Any(u => u.UserName == userName));
        }
    }
}
