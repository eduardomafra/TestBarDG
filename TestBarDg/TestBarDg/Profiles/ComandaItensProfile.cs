using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBarDg.DTOs;
using TestBarDg.Models;

namespace TestBarDg.Profiles
{
    public class ComandaItensProfile : Profile
    {
        public ComandaItensProfile()
        {
            CreateMap<ComandaItens, ComandaItensReadDTO>();
            CreateMap<ComandaItensCreateDTO, ComandaItens>();
            CreateMap<ComandaItensUpdateDTO, ComandaItens>();
            CreateMap<ComandaItens, ComandaItensUpdateDTO>();
        }
    }
}
