using Microsoft.Extensions.DependencyInjection;
using Payroll.Data;
using System;

namespace Payroll.Logic
{
    public static class DIContainer
    {
        public static ServiceProvider Services()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<Employee>();
            AppDbContext dbContext = DbSettings.GetDbContext();
            serviceCollection.AddDbContext<AppDbContext>();
            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork> ();
            // services.AddTransient<>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
