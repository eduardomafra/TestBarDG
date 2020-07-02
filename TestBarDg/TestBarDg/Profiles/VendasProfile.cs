using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBarDg.DTOs;
using TestBarDg.Models;

namespace TestBarDg.Profiles
{
    public class VendasProfile : Profile
    {
        public VendasProfile()
        {
            CreateMap<Venda, VendaReadDTO>();
            CreateMap<VendaCreateDTO, Venda>();
        }
        
    }
}
