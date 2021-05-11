using Battery_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Data.CustomerRepo
{
    public interface ICustomerRepo
    {
        public Task<Customer> GetCustomer(int Id);
        public Task<List<Customer>> GetAllCustomers();
        public Task<bool> AddCustomer(Customer Customer);
    }
}
