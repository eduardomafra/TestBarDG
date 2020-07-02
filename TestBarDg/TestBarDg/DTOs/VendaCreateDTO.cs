using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestBarDg.DTOs
{
    public class VendaCreateDTO
    {
        [Required]
        public double Desconto { get; set; }
        [Required]
        public double ValorTotal { get; set; }
    }
}
