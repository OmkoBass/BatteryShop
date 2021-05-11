using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Dtos
{
    public class AddCustomerDto
    {
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
    }
}
