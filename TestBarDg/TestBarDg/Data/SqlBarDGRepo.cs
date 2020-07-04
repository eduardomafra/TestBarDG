using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBarDg.Models;

namespace TestBarDg.Data
{
    public class SqlBarDGRepo : IBarDGRepo
    {
        private readonly BarDGContext _context;

        public SqlBarDGRepo(BarDGContext context)
        {
            _context = context;
        }

        public void DeletarItensComanda(ComandaItens comandaItens)
        {
            if (comandaItens == null)
            {
                throw new ArgumentNullException(nameof(comandaItens));
            }
            _context.Comanda_Itens.Remove(comandaItens);
        }

        public void FecharComanda(Venda venda)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ComandaItens> GetAllComandaItens()
        {
            return _context.Comanda_Itens.ToList();
        }

        public IEnumerable<ComandaItens> GetAllComandaItensByComanda(int idComanda)
        {
            return _context.Comanda_Itens.Where(w => w.IdComanda == idComanda).ToList();
        }

        public IEnumerable<Comanda> GetAllComandas()
        {
            return _context.Comandas.ToList();
        }

        public IEnumerable<Item> GetAllItens()
        {
            return _context.Itens.ToList();
        }

        public IEnumerable<Venda> GetAllVendas()
        {
            return _context.Vendas.ToList();
        }

        public Comanda GetComandaById(int id)
        {
            return _context.Comandas.FirstOrDefault(p => p.Id == id);
        }

        public ComandaItens GetComandaItensByComanda(int id)
        {
            return _context.Comanda_Itens.FirstOrDefault(p => p.IdComanda == id);
        }

        public ComandaItens GetComandaItensById(int id)
        {
            return _context.Comanda_Itens.FirstOrDefault(p => p.Id == id);
        }

        public Item GetItemById(int id)
        {
            return _context.Itens.FirstOrDefault(p => p.Id == id);
        }

        public Venda GetVendaById(int id)
        {
            return _context.Vendas.FirstOrDefault(p => p.Id == id);
        }

        public void inserirComanda(Comanda comanda)
        {
            if (comanda == null)
            {
                throw new ArgumentNullException(nameof(comanda));
            }

            _context.Comandas.Add(comanda);
        }

        public void inserirItemComanda(ComandaItens comandaItens)
        {
            if (comandaItens == null)
            {
                throw new ArgumentNullException(nameof(comandaItens));
            }

            _context.Comanda_Itens.Add(comandaItens);

        }

        public void updateItensComanda(ComandaItens comandaItens)
        {
            //throw new NotImplementedException();
        }

        public bool saveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void updateComanda(Comanda comanda)
        {
            //throw new NotImplementedException();
        }
    }
}
