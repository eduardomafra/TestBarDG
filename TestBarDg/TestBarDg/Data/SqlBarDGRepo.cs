using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBarDg.Models;
using TestBarDg.Utils;

namespace TestBarDg.Data
{
    public class SqlBarDGRepo : IBarDGRepo
    {
        private readonly BarDGContext _context;
        private readonly IUtilsRepo _utils;

        public SqlBarDGRepo(BarDGContext context, IUtilsRepo utils)
        {
            _context = context;
            _utils = utils;
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

        public IEnumerable<ComandaItens> GetAllComandaItensByComanda(int idComanda)
        {
            return _context.Comanda_Itens.Where(w => w.IdComanda == idComanda).OrderBy(o => o.IdItem).ToList();
        }

        public IEnumerable<Comanda> GetAllComandas()
        {
            if(_context.Comandas.ToList().Count == 0)
            {
                var comandas = _utils.getComandas();

                foreach(Comanda comanda in comandas)
                {
                    inserirComanda(comanda);
                }

                saveChanges();

            }

            return _context.Comandas.ToList();
        }

        public IEnumerable<Item> GetAllItens()
        {
            if(_context.Itens.ToList().Count == 0)
            {
                return _utils.getItens();
            }

            return _context.Itens.ToList();
        }
        public Comanda GetComandaById(int id)
        {
            if (_context.Comandas.ToList().Count == 0)
            {
                var comandas = _utils.getComandas();

                foreach (Comanda comanda in comandas)
                {
                    inserirComanda(comanda);
                }

                saveChanges();

            }

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
            if (_context.Itens.ToList().Count == 0)
            {
                return _utils.getItens().FirstOrDefault(p => p.Id == id);
            }

            return _context.Itens.FirstOrDefault(p => p.Id == id);
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

        public void InserirDesconto(Desconto desconto)
        {
            if (desconto == null)
            {
                throw new ArgumentNullException(nameof(desconto));
            }

            _context.Descontos.Add(desconto);
        }

        public IEnumerable<Desconto> GetDescontosByComandaId(int idComanda)
        {
            return _context.Descontos.Where(w => w.IdComanda == idComanda).OrderBy(o => o.IdItem).ToList();
        }

        public IEnumerable<Desconto> GetAllDescontos()
        {
            return _context.Descontos.ToList();
        }

        public void DeletarDesconto(Desconto desconto)
        {
            if (desconto == null)
            {
                throw new ArgumentNullException(nameof(desconto));
            }
            _context.Descontos.Remove(desconto);
        }
    }
}
