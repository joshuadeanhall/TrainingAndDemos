using Statuos.Domain;
using Statuos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statuos.Service
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        
        public CustomerService(IRepository<Customer> customerRepository, IUnitOfWork uow)
            : base(customerRepository, uow)
        {
            
        }
    }
}
