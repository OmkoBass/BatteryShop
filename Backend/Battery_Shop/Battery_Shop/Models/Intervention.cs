using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Models
{
    public class Intervention
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Range(0, Int16.MaxValue)]
        public int Price { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(64)]
        public string Location { get; set; }

        [MinLength(3)]
        [MaxLength(256)]
        public string Description { get; set; }

        public bool Resolved { get; set; }

        [Required]
        public int BatteryShopId { get; set; }
        public virtual BatteryShop BatteryShop { get; set; }

        [Required]
        public int BatteryId { get; set; }
    }
}
