using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBarDg.DTOs;
using TestBarDg.Models;

namespace TestBarDg.Profiles
{
    public class ItensProfile : Profile
    {
        public ItensProfile()
        {
            CreateMap<Item, ItemReadDTO>();
        }
    }
}
