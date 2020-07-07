using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using TestBarDg.Data;
using TestBarDg.DTOs;
using TestBarDg.Models;
using TestBarDg.Utils;

namespace TestBarDg.Controllers
{
    [Route("api/comandas")]
    [ApiController]
    public class ComandasController : ControllerBase
    {
        private readonly IBarDGRepo _repository;
        private readonly IMapper _mapper;
        private readonly IUtilsRepo _utils;

        public ComandasController(IBarDGRepo repository, IMapper mapper, IUtilsRepo utils)
        {
            _repository = repository;
            _mapper = mapper;
            _utils = utils;
        }

        /// <summary>
        /// Retorna todas as comandas.
        /// </summary>
        /// <response code="200">Retorna todas as comandas</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ComandaReadDTO>> GetAllComandas()
        {
            var comandas = _repository.GetAllComandas();

            return Ok(_mapper.Map<IEnumerable<ComandaReadDTO>>(comandas));
        }

        /// <summary>
        /// Retorna uma comanda de acordo com o id.
        /// </summary>
        /// <returns>Uma comanda de acordo com o id</returns>
        /// <param name="id"></param>
        /// <response code="200">Retorna a comanda referente ao id</response>
        /// <response code="404">Se não existir uma comanda com o id requisitado</response>   
        [HttpGet("{id}", Name = "GetComandaById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ComandaReadDTO> GetComandaById(int id)
        {
            var comanda = _repository.GetComandaById(id);

            if (comanda != null)
            {
                return Ok(_mapper.Map<ComandaReadDTO>(comanda));
            }
            return NotFound();

        }

        /// <summary>
        /// Insere uma comanda.
        /// </summary>
        /// <remarks>
        /// Modelo request:
        ///     
        ///     {
        ///        "isClose": "false"
        ///     }
        ///
        /// </remarks>
        /// <returns>Uma nova comanda criada</returns>
        /// <response code="201">Retorna a nova comanda criada</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<ComandaReadDTO> inserirComanda(ComandaCreateDTO comandaCreateDto)
        {
            var comandaModel = _mapper.Map<Comanda>(comandaCreateDto);
            _repository.inserirComanda(comandaModel);
            _repository.saveChanges();

            var comandaReadDTO = _mapper.Map<ComandaReadDTO>(comandaModel);

            return CreatedAtRoute(nameof(GetComandaById), new { Id = comandaReadDTO.Id }, comandaReadDTO);

        }

        /// <summary>
        /// Atualiza uma comanda específica.
        /// </summary>
        /// <remarks>
        /// Modelo request:
        ///     
        ///     {
        ///        "id": 1,
        ///        "isClose": "true"
        ///     }
        ///
        /// </remarks>
        /// <returns>No content</returns>
        /// <param name="comandaUpdateDTO"></param>  
        /// <response code="204">Comanda atualizada com sucesso</response>
        /// <response code="404">Se o id da comanda não existir</response>     
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult updateComanda(int id, ComandaUpdateDTO comandaUpdateDTO)
        {
            var comandaModelFromRepo = _repository.GetComandaById(id);
            if (comandaModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(comandaUpdateDTO, comandaModelFromRepo);

            _repository.updateComanda(comandaModelFromRepo);

            _repository.saveChanges();

            return NoContent();

        }

        /// <summary>
        /// Fecha uma comanda específica e calcula o desconto.
        /// </summary>
        /// <returns>No content</returns>
        /// <param name="id"></param>  
        /// <response code="204">Comanda fechada com sucesso</response>
        /// <response code="404">Se o id da comanda não existir</response>  
        [HttpPost("fechar/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult FecharComanda(int id)
        {
            var comandaModelFromRepo = _repository.GetComandaById(id);
            var comandaItensListFromRepo = _repository.GetAllComandaItensByComanda(id);

            if (comandaModelFromRepo == null || _utils.comandaItemListIsEmpty(comandaItensListFromRepo) || _utils.verificaSuco(comandaItensListFromRepo))
            {
                return NotFound();
            }

            var descontos = _utils.getDesconto(comandaItensListFromRepo);

            foreach(DescontoCreateDTO desconto in descontos)
            {
                InserirDesconto(desconto);
            }

            comandaModelFromRepo.isClosed = true;
            comandaModelFromRepo.data = DateTime.Now;

            return updateComanda(comandaModelFromRepo.Id, _mapper.Map<ComandaUpdateDTO>(comandaModelFromRepo));
        }

        /// <summary>
        /// Reseta uma comanda específica.
        /// </summary>
        /// <returns>No content</returns>
        /// <param name="id"></param>  
        /// <response code="204">Comanda resetada com sucesso</response>
        /// <response code="404">Se o id da comanda não existir ou se não houver itens na comanda</response>  
        [HttpPost("resetar/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult ResetarComanda(int id)
        {
            var comandaModelFromRepo = _repository.GetComandaById(id);
            var comandaItensListFromRepo = _repository.GetAllComandaItensByComanda(id);
            var descontoListFromRepo = _repository.GetDescontosByComandaId(id);

            if (comandaModelFromRepo == null || comandaItensListFromRepo == null)
            {
                return NotFound();
            }

            foreach(ComandaItens comandaItens in comandaItensListFromRepo)
            {
                _repository.DeletarItensComanda(comandaItens);
            }

            foreach(Desconto descontos in descontoListFromRepo)
            {
                _repository.DeletarDesconto(descontos);
            }

            _repository.saveChanges();

            comandaModelFromRepo.isClosed = false;

            return updateComanda(comandaModelFromRepo.Id, _mapper.Map<ComandaUpdateDTO>(comandaModelFromRepo));
        }

        private void InserirDesconto(DescontoCreateDTO desconto)
        {
            var descontoModel = _mapper.Map<Desconto>(desconto);
            _repository.InserirDesconto(descontoModel);
            _repository.saveChanges();
        }

    }
}
