using GroceryStoreAPI.Entity;
using GroceryStoreAPI.Helper;
using GroceryStoreAPI.Repository;
using GroceryStoreAPI.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace GroceryStoreAPI.Extensions
{
    public static class ServiceExtensions
    {
        // Configure the SQL Database
        public static void ConfigureSQLDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GroceryStoreDBContext>(options => { options.UseSqlServer(configuration.GetValue<string>("DBConnection")); }, ServiceLifetime.Singleton);

        }

        //register the Service to DI the repository and service object
        public static void ConfigureService(this IServiceCollection services)
        {
            //register the repository inorder to DI the repository object and the service object
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<ModelValidationAttribute>();
            services.AddSingleton<DbContext, GroceryStoreDBContext>();

        }
    }
}
