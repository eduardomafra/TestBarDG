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
    [Route("api/itens")]
    [ApiController]
    public class ItensController : ControllerBase
    {
        private readonly IBarDGRepo _repository;
        private readonly IMapper _mapper;

        public ItensController(IBarDGRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todos os itens.
        /// </summary>
        /// <returns>Ok</returns>
        /// <response code="200">Retorna todos os itens</response>    
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ItemReadDTO>> GetAllItens()
        {
            var items = _repository.GetAllItens();

            return Ok(_mapper.Map<IEnumerable<ItemReadDTO>>(items));
        }

        /// <summary>
        /// Retorna um item específica de acordo com o id
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>Ok</returns>
        /// <response code="200">Retorna o item de acordo com o id</response>   
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ItemReadDTO> GetItemById(int id)
        {
            var item = _repository.GetItemById(id);

            if (item != null)
            {
                return Ok(_mapper.Map<ItemReadDTO>(item));
            }
            return NotFound();

        }
    }
}
