using Battery_Shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Data.BatteryShopRepo
{
    public class MockBatteryShopRepo : IBatteryShopRepo
    {
        private readonly DatabaseContext _context;
        public MockBatteryShopRepo(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<BatteryShop> GetBatteryShop(int Id)
        {
            return await _context.BatteryShops.FirstOrDefaultAsync(Bs => Bs.Id == Id);
        }
    }
}
