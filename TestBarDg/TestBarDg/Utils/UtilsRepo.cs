using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBarDg.DTOs;
using TestBarDg.Models;

namespace TestBarDg.Utils
{
    public class UtilsRepo : IUtilsRepo
    {
        public bool comandaItemListIsEmpty(IEnumerable<ComandaItens> comandaItemList)
        {           
            if (!comandaItemList.Any() && comandaItemList != null)
            {
                return true;
            }

            return false;           
        }

        public IEnumerable<DescontoCreateDTO> getDesconto(IEnumerable<ComandaItens> comandaItensList)
        {
            List<DescontoCreateDTO> desconto = new List<DescontoCreateDTO>();

            var cerveja = comandaItensList.Where(w => w.nomeItem == "Cerveja").FirstOrDefault();
            var conhaque = comandaItensList.Where(w => w.nomeItem == "Conhaque").FirstOrDefault();
            var agua = comandaItensList.Where(w => w.nomeItem == "Água").FirstOrDefault();

            if (cerveja != null)
            {
                var quantidadeCerveja = Convert.ToInt32(cerveja.Quantidade / 5);

                if (quantidadeCerveja >= 1)
                {
                    desconto.Add(new DescontoCreateDTO(cerveja.IdComanda, cerveja.IdItem, cerveja.nomeItem, quantidadeCerveja, quantidadeCerveja * cerveja.ValorUnitario));
                }

                if (conhaque != null)
                {
                    if (agua != null)
                    {
                        quantidadeCerveja = Convert.ToInt32(cerveja.Quantidade / 2);
                        var quantidadeConhaque = Convert.ToInt32(conhaque.Quantidade / 3);

                        if (quantidadeCerveja >= 1 && quantidadeConhaque >= 1)
                        {
                            if (quantidadeCerveja <= quantidadeConhaque)
                            {
                                var quantidade = quantidadeCerveja <= agua.Quantidade ? quantidadeCerveja : agua.Quantidade;
                                desconto.Add(new DescontoCreateDTO(agua.IdComanda, agua.IdItem, agua.nomeItem, quantidade, quantidade * agua.ValorUnitario));
                            }
                            else
                            {
                                var quantidade = quantidadeConhaque <= agua.Quantidade ? quantidadeConhaque : agua.Quantidade;
                                desconto.Add(new DescontoCreateDTO(agua.IdComanda, agua.IdItem, agua.nomeItem, quantidade, quantidade * agua.ValorUnitario));
                            }
                        }
                    }
                }
            }

            return desconto;
        }

        public bool verificaSuco(IEnumerable<ComandaItens> comandaItemList)
        {
            return comandaItemList.Where(w => w.nomeItem == "Suco").Select(s => s.Quantidade).FirstOrDefault() > 3 ? true : false;
        }
    }
}
