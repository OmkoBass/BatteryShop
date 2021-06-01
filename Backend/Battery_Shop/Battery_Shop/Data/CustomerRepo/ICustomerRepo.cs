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
        public Task<Customer> GetCustomerByInfo(string Name, string Lastname, string Address);
        public Task<List<Customer>> GetAllCustomers(int BatteryShopId = -1);
        public Task<bool> AddCustomer(Customer Customer);
    }
}
