using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBarDg.Models;

namespace TestBarDg.Data
{
    public interface IBarDGRepo
    {
        bool saveChanges();
        IEnumerable<Item> GetAllItens();
        Item GetItemById(int id);

        IEnumerable<Comanda> GetAllComandas();
        Comanda GetComandaById(int id);
        void inserirComanda(Comanda comanda);
        void updateComanda(Comanda comanda);

        IEnumerable<ComandaItens> GetAllComandaItens();
        IEnumerable<ComandaItens> GetAllComandaItensByComanda(int idComanda);
        ComandaItens GetComandaItensById(int id);
        ComandaItens GetComandaItensByComanda(int id);
        void inserirItemComanda(ComandaItens comandaItens);
        void DeletarItensComanda(ComandaItens comandaItens);
        void updateItensComanda(ComandaItens comandaItens);
        IEnumerable<Venda> GetAllVendas();
        Venda GetVendaById(int id);
        void FecharComanda(Venda venda);
    }
}
