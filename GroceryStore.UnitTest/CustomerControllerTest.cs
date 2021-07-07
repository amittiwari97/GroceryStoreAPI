using GroceryStoreAPI.Controllers;
using GroceryStoreAPI.Entity;
using GroceryStoreAPI.Repository;
using GroceryStoreAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GroceryStoreCustomerTest
{
    public class CustomerControllerTest
    {
        [Fact]
        public async void Get_Customer_By_Id()
        {

            var options = new DbContextOptionsBuilder<GroceryStoreDBContext>()
                             .UseInMemoryDatabase(databaseName: "GroceryStore")
                             .Options;

            using (var context = new GroceryStoreDBContext(options))
            {
                context.Customer.Add(new Customer
                {
                    Id = 1,
                    Name = "Tom"
                });

                context.Customer.Add(new Customer
                {
                    Id = 2,
                    Name = "David"
                });
                context.SaveChanges();
            }

            using (var context = new GroceryStoreDBContext(options))
            {
                Repository<Customer> repo = new Repository<Customer>(context);
                var customer = await repo.Get(2);

                Assert.Equal("2", customer.Id.ToString());
                Assert.Equal("David", customer.Name);
            }
        }


        [Fact]
        public async void GetAll_Customer()
        {

            var options = new DbContextOptionsBuilder<GroceryStoreDBContext>()
                             .UseInMemoryDatabase(databaseName: "GroceryStore")
                             .Options;

            using (var context = new GroceryStoreDBContext(options))
            {
                context.Customer.Add(new Customer
                {
                    Id = 3,
                    Name = "Pat"
                });

                context.Customer.Add(new Customer
                {
                    Id = 4,
                    Name = "Dev"
                });
                context.SaveChanges();
            }

            using (var context = new GroceryStoreDBContext(options))
            {
                Repository<Customer> repo = new Repository<Customer>(context);
                var expectedData = await repo.GetAll();

                var service = new CustomerService(repo);
                var controller = new CustomerController(service);

                var actionResult = await controller.GetAll();
                var Result = actionResult as OkObjectResult;

                var actualData = Result.Value as List<Customer>;
                Assert.Equal((IEnumerable<Customer>)expectedData, actualData);
            }
        }
      
        [Fact]
        public async Task Get_NonExistant_Customer()
        {

            var options = new DbContextOptionsBuilder<GroceryStoreDBContext>()
                             .UseInMemoryDatabase(databaseName: "GroceryStore")
                             .Options;

            using (var context = new GroceryStoreDBContext(options))
            {
                context.Customer.Add(new Customer
                {
                    Id = 5,
                    Name = "Joshu"
                });

                context.Customer.Add(new Customer
                {
                    Id = 6,
                    Name = "Mosh"
                });
                context.SaveChanges();
            }

            using (var context = new GroceryStoreDBContext(options))
            {
                Repository<Customer> repo = new Repository<Customer>(context);
        
                var service = new CustomerService(repo);
                var controller = new CustomerController(service);

                int id = 100;

                var actionResult = await controller.Get(id);

                Assert.IsType<NotFoundResult>(actionResult);

            }

        }

        [Fact]
        public async Task Insert_Customer()
        {
            var options = new DbContextOptionsBuilder<GroceryStoreDBContext>()
                             .UseInMemoryDatabase(databaseName: "GroceryStore")
                             .Options;

            using (var context = new GroceryStoreDBContext(options))
            {
                context.Customer.Add(new Customer
                {
                    Id = 7,
                    Name = "Camran"
                });

                context.Customer.Add(new Customer
                {
                    Id = 8,
                    Name = "Bob"
                });
                context.SaveChanges();
            }

            using (var context = new GroceryStoreDBContext(options))
            {
                Repository<Customer> repo = new Repository<Customer>(context);

                Customer customer = new Customer() { Name = "Swati" };

                await repo.Add(customer);

                Assert.Contains(await repo.GetAll(), item => item == customer);
            }
        }

        [Fact]
        public async void Update_Customer()
        {
            var options = new DbContextOptionsBuilder<GroceryStoreDBContext>()
                             .UseInMemoryDatabase(databaseName: "GroceryStore")
                             .Options;

            using (var context = new GroceryStoreDBContext(options))
            {
                context.Customer.Add(new Customer
                {
                    Id = 10,
                    Name = "Kavin"
                });

                context.Customer.Add(new Customer
                {
                    Id = 11,
                    Name = "Jen"
                });
                context.SaveChanges();
            }

            using (var context = new GroceryStoreDBContext(options))
            {
                Repository<Customer> repo = new Repository<Customer>(context);
                int id = 11;
                var name = await repo.Get(11);
                string changeToName = String.Concat(name.Name.ToString(), "Modified");
                Customer customer = new Customer() { Id = id, Name = changeToName };

                await repo.Update(customer, customer.Id);

                var updatedName = await repo.Get(11);

                Assert.Equal(changeToName, updatedName.Name.ToString());

            }    

        }        
    }
}
