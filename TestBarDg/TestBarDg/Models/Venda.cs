using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestBarDg.Models
{
    public class Venda
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double Desconto { get; set; }
        [Required]
        public double ValorTotal { get; set; }
    }
}
