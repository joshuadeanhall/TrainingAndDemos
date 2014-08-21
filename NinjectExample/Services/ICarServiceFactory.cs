using System.Collections.Generic;

namespace NinjectExample.Services
{
    public interface ICarServiceFactory
    {
        //Get and Create prefix acts differently
        ICarService GetFordService();
        ICarService GetGmService();
        ICarService GetBmwService();

        IFordService CreateFordService();
        IBmwService CreateBmwService();

        List<ICarService> CreateAllCarServices();

    }
}
