using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NinjectExample.Services
{
    public class BmwService : IBmwService
    {

        public void ChangeOil()
        {
            throw new NotImplementedException();
        }
    }

    public interface IBmwService : ICarService
    {
    }
}