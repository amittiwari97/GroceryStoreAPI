using GroceryStoreAPI.Entity;
using GroceryStoreAPI.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customer;

        public CustomerService(IRepository<Customer> customer)
        {
            _customer = customer;
        }       
        public async Task<Customer> Get(int id)
        {
           return await _customer.Get(id);
        }
        public async Task<List<Customer>> GetAll()
        {
            return await _customer.GetAll();
        }
        public async Task Add(Customer customer)
        {
             await _customer.Add(customer);
        }
        public async Task Update(Customer customer)
        {
            await _customer.Update(customer,customer.Id);
        }      
    }
}
    