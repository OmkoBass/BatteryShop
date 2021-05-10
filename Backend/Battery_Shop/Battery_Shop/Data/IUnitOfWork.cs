using Battery_Shop.Data.AuthRepo;
using Battery_Shop.Data.EmployeeRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Data
{
    public interface IUnitOfWork
    {
        public IEmployeeRepo IEmployeeRepo { get; set; }
        public IAuthRepo IAuthRepo { get; set; }
        Task <bool> Complete();
    }
}
