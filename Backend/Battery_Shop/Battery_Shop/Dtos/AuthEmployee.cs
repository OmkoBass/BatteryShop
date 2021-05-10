using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Dtos
{
    public class AuthEmployee
    {
        [Required]
        [MinLength(3)]
        [MaxLength(32)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(32)]
        public string Password { get; set; }
    }
}
