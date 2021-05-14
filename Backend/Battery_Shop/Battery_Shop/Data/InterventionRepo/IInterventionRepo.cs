using Battery_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Data
{
    public interface IInterventionRepo
    {
        public Task<List<Intervention>> GetAllInterventionsByBatteryShop(int BatteryShopId);
        public Task<Intervention> GetIntervention(int Id);
        public Task<bool> AddIntervention(Intervention Intervention);
    }
}
