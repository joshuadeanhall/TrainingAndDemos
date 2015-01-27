using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NinjectExample.Services
{
    public class FordService : IFordService
    {
        public void ChangeOil()
        {
            throw new NotImplementedException();
        }
    }

    public interface IFordService : ICarService
    {
    }
}