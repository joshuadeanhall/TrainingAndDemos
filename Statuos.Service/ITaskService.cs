using Statuos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Statuos.Service
{
    public interface ITaskService : IBaseService<Task>
    {
        void DeleteAssignedUser(Task task, User user);
    }
}
