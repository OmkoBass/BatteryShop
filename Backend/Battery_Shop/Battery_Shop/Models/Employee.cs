using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Models
{
    public class Employee
    {
        public enum JobType
        {
            service = 0,
            sales = 1,
            supply = 2,
            intervention = 3,
            administration = 4
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(16)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(64)]
        public string Password { get; set; }

        //[Required]
        [MinLength(3)]
        [MaxLength(16)]
        public string Name { get; set; }

        //[Required]
        [MinLength(3)]
        [MaxLength(32)]
        public string LastName { get; set; }

        [Required]
        public JobType Job { get; set; }

        [Required]
        public int BatteryShopId { get; set; }
        public virtual BatteryShop BatteryShop { get; set; }
    }
}
