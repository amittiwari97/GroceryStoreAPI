using GroceryStoreAPI.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Service
{
    public interface ICustomerService
    {
        public Task<List<Customer>> GetAll();
        public Task<Customer> Get(int id);
        public Task Add(Customer customer);
        public Task Update(Customer customer);
    }
}
