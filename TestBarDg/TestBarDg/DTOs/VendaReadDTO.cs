using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestBarDg.DTOs
{
    public class VendaReadDTO
    {
        public int Id { get; set; }
        [Required]
        public double Desconto { get; set; }
        [Required]
        public double ValorTotal { get; set; }
    }
}
