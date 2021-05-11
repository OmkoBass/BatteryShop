using Battery_Shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Data.CustomerRepo
{
    public class MockCustomerRepo : ICustomerRepo
    {
        private readonly DatabaseContext _context;
        public MockCustomerRepo(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCustomer(Customer Customer)
        {
            await _context.Customers.AddAsync(Customer);
            return true;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomer(int Id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id == Id);
        }
    }
}
