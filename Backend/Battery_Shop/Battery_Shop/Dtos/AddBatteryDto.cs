using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Dtos
{
    public class AddBatteryDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(32)]
        public string Name { get; set; }

        [Range(0, Int16.MaxValue)]
        public int Price { get; set; }

        [Required]
        public int StorageId { get; set; }
    }
}
