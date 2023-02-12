
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Payroll.Data;
using Unity.Lifetime;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PayrollLogic;

namespace Payroll
{

    public static class Bootstrapper 
    {
       /*
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // e.g. container.RegisterType<ITestService, TestService>();   
           // container.RegisterType<Employee>();
           // container.RegisterType(typeof(IEmployee), typeof(Employee));
           // container.RegisterType<IEmployeeRepository, EmployeeRepository>(new TransientLifetimeManager());
            //  container.RegisterType(typeof(IUnitOfWork), typeof(UnitOfWork));
            container.RegisterType<IUnitOfWork, UnitOfWork>(new TransientLifetimeManager());
            return container;
        }
   */
    }

}
