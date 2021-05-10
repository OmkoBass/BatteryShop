using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Dtos
{
    public class AddStorageDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(32)]
        public string Name { get; set; }
    }
}
