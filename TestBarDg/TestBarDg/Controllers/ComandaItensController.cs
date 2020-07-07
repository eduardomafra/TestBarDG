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

        /// <summary>
        /// Retorna todos os itens presentes em todas as comandas.
        /// </summary>
        /// <param name="id"></param>  
        [HttpGet]
        public ActionResult<IEnumerable<ComandaItensReadDTO>> GetAllComandaItens()
        {
            var comandaItens = _repository.GetAllComandaItens();

            return Ok(_mapper.Map<IEnumerable<ComandaItensReadDTO>>(comandaItens));
        }

        /// <summary>
        /// Retorna um item inserido em uma comanda de acordo com o id.
        /// </summary>
        /// <param name="id"></param>  
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

        /// <summary>
        /// Retorna todos os itens de uma comanda específica.
        /// </summary>
        /// <param name="id"></param>  
        [HttpGet("comanda/{idComanda}", Name = "GetAllComandaItensByComanda")]
        public ActionResult<IEnumerable<ComandaItensReadDTO>> GetAllComandaItensByComanda(int idComanda)
        {
            var comandaItens = _repository.GetAllComandaItensByComanda(idComanda);

            if (comandaItens != null)
            {
                return Ok(_mapper.Map<IEnumerable<ComandaItensReadDTO>>(comandaItens));
            }
            return NotFound();

        }

        /// <summary>
        /// Insere um item em uma comanda específica.
        /// </summary>
        /// <param name="id"></param>  
        [HttpPost]
        public ActionResult<ComandaItensReadDTO> inserirItemComanda(ComandaItensCreateDTO comandaItensCreateDto)
        {
            var comandaItensModel = _mapper.Map<ComandaItens>(comandaItensCreateDto);
            _repository.inserirItemComanda(comandaItensModel);
            _repository.saveChanges();

            var comandaReadDTO = _mapper.Map<ComandaItensReadDTO>(comandaItensModel);

            return CreatedAtRoute(nameof(GetComandaItensById), new { Id = comandaReadDTO.IdComanda }, comandaReadDTO);

        }

        /// <summary>
        /// Deleta um item de uma comanda específica.
        /// </summary>
        /// <param name="id"></param>  
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

        /// <summary>
        /// Atualiza um item inserido em uma comanda específica.
        /// </summary>
        /// <param name="id"></param>  
        [HttpPut("{id}")]
        public ActionResult updateItensComanda(int id, ComandaItensUpdateDTO comandaItensUpdateDTO)
        {
            var itensComandaModelFromRepo = _repository.GetComandaItensById(id);
            if (itensComandaModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(comandaItensUpdateDTO, itensComandaModelFromRepo);

            _repository.updateItensComanda(itensComandaModelFromRepo);

            _repository.saveChanges();

            return NoContent();

        }

    }
}
