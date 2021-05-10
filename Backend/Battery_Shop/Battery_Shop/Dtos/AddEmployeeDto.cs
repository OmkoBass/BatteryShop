using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Dtos
{
    public class AddEmployeeDto
    {
        public enum JobType
        {
            service = 0,
            sales = 1,
            supply = 2,
            intervention = 3,
            administration = 4
        }

        [Required]
        [MinLength(3)]
        [MaxLength(16)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(64)]
        public string Password { get; set; }

        //[Required]
        //[MinLength(3)]
        //[MaxLength(16)]
        public string Name { get; set; }

        //[Required]
        //[MinLength(3)]
        //[MaxLength(32)]
        public string LastName { get; set; }

        [Required]
        public JobType Job { get; set; }

        public int BatteryShopId { get; set; }
    }
}
