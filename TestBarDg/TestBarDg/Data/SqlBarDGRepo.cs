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

        public IEnumerable<ComandaItens> GetAllComandaItens()
        {
            return _context.Comanda_Itens.ToList();
        }

        public IEnumerable<Comanda> GetAllComandas()
        {
            return _context.Comandas.ToList();
        }

        public IEnumerable<Item> GetAllItens()
        {
            return _context.Itens.ToList();
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

        public void inserirItemComanda(ComandaItens comandaItens)
        {
            if (comandaItens == null)
            {
                throw new ArgumentNullException(nameof(comandaItens));
            }

            _context.Comanda_Itens.Add(comandaItens);

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
