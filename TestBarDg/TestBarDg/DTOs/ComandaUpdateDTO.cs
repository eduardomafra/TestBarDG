using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestBarDg.DTOs
{
    public class ComandaUpdateDTO
    {
        [Required]
        public bool isClosed { get; set; }
        public DateTime data { get; set; }
    }
}
