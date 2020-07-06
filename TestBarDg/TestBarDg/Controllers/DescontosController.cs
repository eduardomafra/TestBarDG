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
    [Route("api/descontos")]
    [ApiController]
    public class DescontosController : ControllerBase
    {
        private readonly IBarDGRepo _repository;
        private readonly IMapper _mapper;

        public DescontosController(IBarDGRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DescontoReadDTO>> GetAllDescontos()
        {
            var descontos = _repository.GetAllDescontos();

            return Ok(_mapper.Map<IEnumerable<DescontoReadDTO>>(descontos));
        }

        [HttpGet("comanda/{idComanda}", Name = "GetDescontosByComandaId")]
        public ActionResult<IEnumerable<DescontoReadDTO>> GetDescontosByComandaId(int idComanda)
        {
            var descontoItens = _repository.GetDescontosByComandaId(idComanda);

            if (descontoItens != null)
            {
                return Ok(_mapper.Map<IEnumerable<DescontoReadDTO>>(descontoItens));
            }
            return NotFound();

        }

    }
}
