using Battery_Shop.Data.AuthRepo;
using Battery_Shop.Data.EmployeeRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Data
{
    public class MockUnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        public IEmployeeRepo IEmployeeRepo { get; set; }
        public IAuthRepo IAuthRepo { get; set; }
        public MockUnitOfWork(
            DatabaseContext context,
            IEmployeeRepo employeeRepo,
            IAuthRepo authRepo
        )
        {
            _context = context;
            IEmployeeRepo = employeeRepo;
            IAuthRepo = authRepo;
        }   
        
        public async Task<bool> Complete()
        {
            var save = await _context.SaveChangesAsync();

            if (save == 1)
                return true;
            return false;
        }
    }
}
