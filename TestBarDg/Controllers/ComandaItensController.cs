using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBarDg.Data;
using TestBarDg.DTOs;
using TestBarDg.Models;

namespace TestBarDg.Controllers
{
    [Route("api/comanda_itens")]
    [ApiController]
    public class ComandaItensController : ControllerBase
    {

        private readonly IBarDGRepo _repository;
        private readonly IMapper _mapper;

        public ComandaItensController(IBarDGRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ComandaItensReadDTO>> GetAllComandaItens()
        {
            var comandaItens = _repository.GetAllComandaItens();

            return Ok(_mapper.Map<IEnumerable<ComandaItensReadDTO>>(comandaItens));
        }

        [HttpGet("{id}", Name = "GetComandaItensById")]
        public ActionResult<ComandaItensReadDTO> GetComandaItensById(int id)
        {
            var comandaItens = _repository.GetComandaItensById(id);

            if (comandaItens != null)
            {
                return Ok(_mapper.Map<ComandaItensReadDTO>(comandaItens));
            }
            return NotFound();

        }

        [HttpGet("{id}", Name= "GetComandaItensByComanda")]
        public ActionResult<ComandaItensReadDTO> GetComandaItensByComanda(int id)
        {
            var comandaItens = _repository.GetComandaItensByComanda(id);

            if (comandaItens != null)
            {
                return Ok(_mapper.Map<ComandaItensReadDTO>(comandaItens));
            }
            return NotFound();

        }

        [HttpPost]
        public ActionResult<ComandaItensReadDTO> inserirItemComanda(ComandaItensCreateDTO comandaItensCreateDto)
        {
            var comandaItensModel = _mapper.Map<ComandaItens>(comandaItensCreateDto);
            _repository.inserirItemComanda(comandaItensModel);
            _repository.saveChanges();

            var comandaReadDTO = _mapper.Map<ComandaItensReadDTO>(comandaItensModel);

            return CreatedAtRoute(nameof(GetComandaItensByComanda), new { Id = comandaReadDTO.IdComanda }, comandaReadDTO);

        }

        [HttpDelete("{id}")]
        public ActionResult DeletarItensComanda(int id)
        {
            var itensComandaModelFromRepo = _repository.GetComandaItensById(id);
            if(itensComandaModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeletarItensComanda(itensComandaModelFromRepo);
            _repository.saveChanges();

            return NoContent();

        }
    }
}
