using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Dtos
{
    public class AddInterventionDto
    {
        [Required]
        public int BatteryId { get; set; }

        [Required]
        [Range(0, Int16.MaxValue)]
        public int Price { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(64)]
        public string Location { get; set; }
    }
}
