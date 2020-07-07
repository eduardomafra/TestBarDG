using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestBarDg.Models
{
    public class Comanda
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool isClosed { get; set; }

        public DateTime data { get; set; }
    }
}
