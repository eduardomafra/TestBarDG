using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestBarDg.DTOs
{
    public class ComandaCreateDTO
    {
        [Required]
        public bool isClosed { get; set; }
    }
}
