using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBarDg.DTOs;
using TestBarDg.Models;

namespace TestBarDg.Utils
{
    public interface IUtilsRepo
    {

        bool comandaItemListIsEmpty(IEnumerable<ComandaItens> comandaItemList);
        IEnumerable<DescontoCreateDTO> getDesconto(IEnumerable<ComandaItens> comandaItensList);
        bool verificaSuco(IEnumerable<ComandaItens> comandaItemList);

    }
}
