using Battery_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Dtos
{
    public class StorageDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<BatteryDto> Batteries { get; set; }

        public int BatterShopId { get; set; }
    }
}
