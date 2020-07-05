using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestBarDg.DTOs
{
    public class DescontoCreateDTO
    {
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

        public DescontoCreateDTO(int IdComanda, int IdItem, string NomeItem, int Quantidade, double ValorDesconto)
        {
            this.IdComanda = IdComanda;
            this.IdItem = IdItem;
            this.NomeItem = NomeItem;
            this.Quantidade = Quantidade;
            this.ValorDesconto = ValorDesconto;
        }

    }
}
