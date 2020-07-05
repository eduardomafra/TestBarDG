﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestBarDg.DTOs
{
    public class ComandaItensUpdateDTO
    {
        [Required]
        public int IdComanda { get; set; }

        [Required]
        public int IdItem { get; set; }

        [Required]
        public string nomeItem { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public double ValorUnitario { get; set; }

        [Required]
        public double ValorTotal { get; set; }
    }
}
