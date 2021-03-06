using Battery_Shop.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Models
{
    public class Battery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(32)]
        public string Name { get; set; }

        [Range(0, Int16.MaxValue)]
        public int Price { get; set; }

        [Range(0, 100)]
        public int Life { get; set; }

        [DataType(DataType.Date)]
        public DateTime Warrant { get; set; }

        public bool Sold { get; set; }

        public bool Replacement { get; set; }

        [Required]
        public int StorageId { get; set; }
        public virtual Storage Storage { get; set; }

        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int BatteryShopId { get; set; }
        public virtual BatteryShop BatteryShop { get; set; }
    }
}
