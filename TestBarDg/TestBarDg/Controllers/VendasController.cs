using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBarDg.Data;
using TestBarDg.DTOs;

namespace TestBarDg.Controllers
{
    [Route("api/vendas")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly IBarDGRepo _repository;
        private readonly IMapper _mapper;

        public VendasController(IBarDGRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VendaReadDTO>> GetAllVendas()
        {
            var vendas = _repository.GetAllVendas();

            return Ok(_mapper.Map<IEnumerable<VendaReadDTO>>(vendas));
        }

        [HttpGet("{id}", Name = "GetVendaById")]
        public ActionResult<VendaReadDTO> GetVendaById(int id)
        {
            var venda = _repository.GetVendaById(id);

            if (venda != null)
            {
                return Ok(_mapper.Map<VendaReadDTO>(venda));
            }
            return NotFound();

        }

        /*[HttpPost]
        public ActionResult<ComandaItensReadDTO> FecharComanda(Venda venda)
        {
            var comandaItensModel = _mapper.Map<ComandaItens>(comandaItensCreateDto);
            _repository.inserirItemComanda(comandaItensModel);
            _repository.saveChanges();

            var comandaReadDTO = _mapper.Map<ComandaItensReadDTO>(comandaItensModel);

            return CreatedAtRoute(nameof(GetComandaItensByComanda), new { Id = comandaReadDTO.IdComanda }, comandaReadDTO);

        }*/
    }
}
