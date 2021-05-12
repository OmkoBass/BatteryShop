using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Dtos
{
    public class BatteryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public int Life { get; set; }

        public DateTime Warrant { get; set; }

        public bool Sold { get; set; }

        public int StorageId { get; set; }
        public int? CustomerId { get; set; }
    }
}
