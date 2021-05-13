using Battery_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Data.BatteryRepo
{
    public interface IBatteryRepo
    {
        public Task<List<Battery>> GetAllBatteries();
        public Task<Battery> GetBattery(int Id);
        public Task<List<Battery>> GetBatteriesByBatteryShop(int BatteryShopId);
        public Task<bool> AddBattery(Battery Battery);
        public bool UpdateBattery(Battery Battery);
        public bool DeleteBattery(Battery Battery);
    }
}
