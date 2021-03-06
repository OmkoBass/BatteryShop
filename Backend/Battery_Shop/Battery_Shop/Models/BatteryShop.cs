using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Models
{
    public class BatteryShop
    {
        public BatteryShop() 
        {
            this.Employees = new HashSet<Employee>();
            this.Storages = new HashSet<Storage>();
            this.Customers = new HashSet<Customer>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(16)]
        public string Name { get; set; }

        [MinLength(3)]
        [MaxLength(64)]
        public string Address { get; set; }

        [Required]
        [MinLength(9)]
        [MaxLength(11)]
        public string Phone { get; set; }

        public ICollection<Employee> Employees { get; set; }
        public ICollection<Storage> Storages { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
