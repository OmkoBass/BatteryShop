using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Batteries = new HashSet<Battery>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(16)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(32)]
        public string LastName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(64)]
        public string Address { get; set; }

        public ICollection<Battery> Batteries { get; set; }

        [Required]
        public int BatteryShopId { get; set; }
        public virtual BatteryShop BatteryShop { get; set; }
    }
}
