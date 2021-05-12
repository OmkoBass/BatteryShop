using Battery_Shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Data.EmployeeRepo
{
    public class MockEmployeeRepo : IEmployeeRepo
    {
        private readonly DatabaseContext _context;
        public MockEmployeeRepo(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddEmployee(Employee Employee)
        {
            await _context.Employees.AddAsync(Employee);
            return true;
        }

        public bool DeleteEmployee(Employee Employee)
        {
            _context.Employees.Remove(Employee);
            return true;
        }

        public async Task<List<Employee>> GetAllByBatteryShopId(int BatteryShopId)
        {
            return await _context.Employees.Where(e => e.BatteryShopId == BatteryShopId).ToListAsync();
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployee(int EmployeeId)
        {
            return await _context.Employees.FindAsync(EmployeeId);
        }

        public bool UpdateEmployee(Employee Employee)
        {
            _context.Employees.Update(Employee);
            return true;
        }
    }
}
