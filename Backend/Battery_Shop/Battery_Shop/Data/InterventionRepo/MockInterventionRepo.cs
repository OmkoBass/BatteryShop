using Battery_Shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Data.InterventionRepo
{
    public class MockInterventionRepo : IInterventionRepo
    {
        private readonly DatabaseContext _context;

        public MockInterventionRepo(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddIntervention(Intervention Intervention)
        {
            await _context.Interventions.AddAsync(Intervention);
            return true;
        }

        public async Task<List<Intervention>> GetAllInterventionsByBatteryShop(int BatteryShopId)
        {
            return await _context.Interventions.Where(i => i.BatteryShopId == BatteryShopId).ToListAsync();
        }

        public async Task<Intervention> GetIntervention(int Id)
        {
            return await _context.Interventions.FirstOrDefaultAsync(i => i.Id == Id);
        }
    }
}
