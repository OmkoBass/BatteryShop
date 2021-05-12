using Battery_Shop.Data.AuthRepo;
using Battery_Shop.Data.BatteryRepo;
using Battery_Shop.Data.BatteryShopRepo;
using Battery_Shop.Data.CustomerRepo;
using Battery_Shop.Data.EmployeeRepo;
using Battery_Shop.Data.StorageRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Data
{
    public class MockUnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        public IBatteryShopRepo IBatteryShopRepo { get; set; }
        public IEmployeeRepo IEmployeeRepo { get; set; }
        public IStorageRepo IStorageRepo { get; set; }
        public IBatteryRepo IBatteryRepo { get; set; }
        public ICustomerRepo ICustomerRepo { get; set; }
        public IAuthRepo IAuthRepo { get; set; }
        public MockUnitOfWork(
            DatabaseContext context,
            IBatteryShopRepo batteryShopRepo,
            IEmployeeRepo employeeRepo,
            IStorageRepo storageRepo,
            IBatteryRepo batteryRepo,
            ICustomerRepo customerRepo,
            IAuthRepo authRepo
        )
        {
            _context = context;
            IBatteryShopRepo = batteryShopRepo;
            IEmployeeRepo = employeeRepo;
            IStorageRepo = storageRepo;
            IBatteryRepo = batteryRepo;
            ICustomerRepo = customerRepo;
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
