using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Services;

namespace WebApplication.App_Start
{
    public class NinjectDependencyInjection : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyInjection(IKernel kernel)
        {
            this.kernel = kernel;

        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void Addbinding(IKernel kernel)
        {
            kernel.Bind<IReservationService>().To<ReservationService>();
        }
    }
}