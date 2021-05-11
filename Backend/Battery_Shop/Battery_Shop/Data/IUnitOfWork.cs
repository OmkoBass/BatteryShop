using Battery_Shop.Data.AuthRepo;
using Battery_Shop.Data.BatteryRepo;
using Battery_Shop.Data.CustomerRepo;
using Battery_Shop.Data.EmployeeRepo;
using Battery_Shop.Data.StorageRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Data
{
    public interface IUnitOfWork
    {
        public IEmployeeRepo IEmployeeRepo { get; set; }
        public IStorageRepo IStorageRepo { get; set; }
        public IBatteryRepo IBatteryRepo { get; set; }
        public ICustomerRepo ICustomerRepo { get; set; }
        public IAuthRepo IAuthRepo { get; set; }
        Task <bool> Complete();
    }
}
