using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Payroll.Data;

namespace Payroll.Logic
{
    public static class DbSettings 
    {
        public static AppDbContext GetDbContext() 
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(GetConnectionString("default"));
            return new AppDbContext(optionsBuilder.Options);
        }

        private static String GetConnectionString(string connectionName)
        {
             var cc = new ConfigurationBuilder().SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                                            .AddJsonFile("appSettings.json").Build();
            string ConnectionString = cc.GetConnectionString(connectionName);
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            return ConnectionString;
        }
    }
}
