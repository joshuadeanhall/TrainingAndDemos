using Statuos.Data;
using Statuos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Statuos.Service
{
    public class TaskService : BaseService<Task>, ITaskService
    {
        public TaskService(IRepository<Task> repository, IUnitOfWork uow) : base(repository, uow) { }

        public void DeleteAssignedUser(Task task, User user)
        {
            task.Users.Remove(user);
            _uow.Save();

        }
    }
}
