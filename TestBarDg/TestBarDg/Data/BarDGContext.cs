using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBarDg.Models;

namespace TestBarDg.Data
{
    public class BarDGContext : DbContext
    {
        public BarDGContext(DbContextOptions<BarDGContext> opt) : base(opt)
        {

        }

        public DbSet<Item> Itens { get; set; }
        public DbSet<Comanda> Comandas { get; set; }
        public DbSet<ComandaItens> Comanda_Itens { get; set; }
        public DbSet<Desconto> Descontos { get; set; }
    }
}
