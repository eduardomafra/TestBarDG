using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestBarDg.Models
{
    public class Desconto
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int IdComanda { get; set; }

        [Required]
        public int IdItem { get; set; }

        [Required]
        public string NomeItem { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public double ValorDesconto { get; set; }

        
    }
}
