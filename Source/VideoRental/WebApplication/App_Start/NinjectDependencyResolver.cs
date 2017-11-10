using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Services;

namespace WebApplication.App_Start
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IAccountService>().To<AccountService>();

            kernel.Bind<IRentAndReturnDiskService>().To<RentAndReturnDiskService>();
            kernel.Bind<IReservationService>().To<ReservationService>();
            kernel.Bind<ILateChargesServices>().To<LateChargesService>();
            //customer report or title report
            kernel.Bind<ICustomerReportService>().To<CustomerReportService>();
            kernel.Bind<IStatisticReportService>().To<StatisticReportService>();
            //show information disk or title
            kernel.Bind<IDiskManagementService>().To<DiskManagementService>();
            kernel.Bind<ITitleManagementService>().To<TitleManagementService>();
            //create, update, delete Customer, Disk, DiskTitle, RentalRate
            kernel.Bind<ICustomerService>().To<CustomerService>();
            kernel.Bind<IDiskService>().To<DiskService>();
            kernel.Bind<IDiskTitleService>().To<DiskTitleService>();
            kernel.Bind<IRentalRate>().To<RentalRateService>();
        }
    }
}