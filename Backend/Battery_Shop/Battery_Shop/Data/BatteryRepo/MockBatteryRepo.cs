using Battery_Shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Data.BatteryRepo
{
    public class MockBatteryRepo : IBatteryRepo
    {
        private readonly DatabaseContext _context;

        public MockBatteryRepo(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddBattery(Battery Battery)
        {
            await _context.Batteries.AddAsync(Battery);
            return true;
        }

        public bool DeleteBattery(Battery Battery)
        {
            _context.Batteries.Remove(Battery);
            return true;
        }

        public async Task<List<Battery>> GetAllBatteries()
        {
            return await _context.Batteries.ToListAsync();
        }

        public async Task<Battery> GetBattery(int Id)
        {
            return await _context.Batteries.FirstOrDefaultAsync(b => b.Id == Id);
        }

        public bool UpdateBattery(Battery Battery)
        {
            _context.Batteries.Update(Battery);
            return true;
        }
    }
}
