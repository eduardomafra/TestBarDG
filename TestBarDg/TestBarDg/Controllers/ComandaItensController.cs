using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        /// <returns>Ok</returns>
        /// <response code="200">Retorna todos os itens registrados em comandas</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ComandaItensReadDTO>> GetAllComandaItens()
        {
            var comandaItens = _repository.GetAllComandaItens();

            return Ok(_mapper.Map<IEnumerable<ComandaItensReadDTO>>(comandaItens));
        }

        /// <summary>
        /// Retorna um item inserido em uma comanda de acordo com o id.
        /// </summary> 
        /// <returns>Ok</returns>
        /// <param name="id"></param>  
        /// <response code="200">Retorna um item registrado em uma comanda de acordo com o id</response>
        /// <response code="404">Se o id do item na comanda não existir</response>  
        [HttpGet("{id}", Name = "GetComandaItensById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <returns>Ok</returns>
        /// <param name="idComanda"></param>  
        /// <response code="200">Itens encontrados</response>
        /// <response code="404">Se o id da comanda não existir</response>  
        [HttpGet("comanda/{idComanda}", Name = "GetAllComandaItensByComanda")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <param name="comandaItensCreateDto"></param>  
        /// <summary>
        /// Insere uma comanda.
        /// </summary>
        /// <remarks>
        /// Modelo request:
        ///     
        ///     {
        ///         "idComanda": 4,
        ///         "idItem": 3,
        ///         "nomeItem": "Suco",
        ///         "quantidade": 4,
        ///         "valorUnitario": 50.0,
        ///         "valorTotal": 200.0
        ///     }
        ///
        /// </remarks>
        /// <returns>Um novo item inserido em uma comanda</returns>
        /// <response code="201">Retorna o novo item registrado para a comanda</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<ComandaItensReadDTO> inserirItemComanda(ComandaItensCreateDTO comandaItensCreateDto)
        {
            var comandaItensModel = _mapper.Map<ComandaItens>(comandaItensCreateDto);
            _repository.inserirItemComanda(comandaItensModel);
            _repository.saveChanges();

            var comandaReadDTO = _mapper.Map<ComandaItensReadDTO>(comandaItensModel);

            return CreatedAtRoute(nameof(GetComandaItensById), new { Id = comandaReadDTO.IdComanda }, comandaReadDTO);

        }

        /// <summary>
        /// Deleta um item de uma comanda.
        /// </summary>
        /// <param name="id"></param>  
        /// <returns>NoContent</returns>
        /// <response code="204">Item deletado</response>  
        /// <response code="404">Item não encontrado</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// Atualiza um item inserido em uma comanda.
        /// </summary>
        /// <param name="comandaItensUpdateDTO"></param>  
        /// <remarks>
        /// Modelo request:
        ///     
        ///     {
        ///         "id": 1,
        ///         "idComanda": 4,
        ///         "idItem": 3,
        ///         "nomeItem": "Suco",
        ///         "quantidade": 4,
        ///         "valorUnitario": 50.0,
        ///         "valorTotal": 200.0
        ///     }
        ///
        /// </remarks>
        /// <returns>No content</returns>
        /// <response code="204">Item atualizado</response>   
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
