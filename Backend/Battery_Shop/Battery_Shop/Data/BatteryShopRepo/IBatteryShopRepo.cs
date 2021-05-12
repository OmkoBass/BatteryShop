using Battery_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Data.BatteryShopRepo
{
    public interface IBatteryShopRepo
    {
        public Task<BatteryShop> GetBatteryShop(int Id);
    }
}
