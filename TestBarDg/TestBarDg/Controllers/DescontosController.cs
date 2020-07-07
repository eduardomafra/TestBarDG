using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Retorna todos os descontos.
        /// </summary> 
        /// <returns>Ok</returns>
        /// <response code="200">Retorna todos os descontos</response>   
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<DescontoReadDTO>> GetAllDescontos()
        {
            var descontos = _repository.GetAllDescontos();

            return Ok(_mapper.Map<IEnumerable<DescontoReadDTO>>(descontos));
        }

        /// <summary>
        /// Retorna todos os descontos de uma comanda específica.
        /// </summary>
        /// <param name="id"></param>  
        /// <returns>Desconto de acordo com comanda</returns>
        /// <response code="200">Retorna todos os descontos de determinada comanda</response>   
        /// <response code="404">Comanda não encontrada</response> 
        [HttpGet("comanda/{idComanda}", Name = "GetDescontosByComandaId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
