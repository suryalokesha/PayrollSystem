using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payroll.Data;

namespace Payroll.ConsoleApp
{
    public class PayrollConnectionSettings 
    {
        private string _connectionString = null;// { get; set; }

        public IServiceCollection PayrollServices(string connectionName) 
        {
            var serviceCollection = new ServiceCollection();
            _connectionString = GetConnection(connectionName);
           return serviceCollection.AddDbContext<AppDbContext>(options =>options.UseSqlServer(_connectionString));
        }

        private  String GetConnection(string connectionName)
        {
            IConfiguration _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                                            .AddJsonFile("appSettings.json").Build();
            string ConnectionString = _configuration.GetConnectionString(connectionName);
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            return ConnectionString;
        }
    }
}
