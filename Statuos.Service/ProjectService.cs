using Statuos.Data;
using Statuos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statuos.Service
{
    public class ProjectService : BaseService<Project>, IProjectService
    {
        public ProjectService(IRepository<Project> repository, IUnitOfWork uow) : base(repository, uow) { }
    }
}
