using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBarDg.DTOs;
using TestBarDg.Models;

namespace TestBarDg.Profiles
{
    public class ComandasProfile : Profile
    {
        public ComandasProfile()
        {
            CreateMap<Comanda, ComandaReadDTO>();
            CreateMap<ComandaUpdateDTO, Comanda>();
            CreateMap<Comanda, ComandaUpdateDTO>();
        }
    }
}
