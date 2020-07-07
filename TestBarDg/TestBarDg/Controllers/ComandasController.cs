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
        /// <param name="id"></param>  
        [HttpGet]
        public ActionResult<IEnumerable<ComandaReadDTO>> GetAllComandas()
        {
            var comandas = _repository.GetAllComandas();

            return Ok(_mapper.Map<IEnumerable<ComandaReadDTO>>(comandas));
        }

        /// <summary>
        /// Retorna uma comanda de acordo com o id.
        /// </summary>
        // <returns>Uma comanda de acordo com o id</returns>
        /// <param name="comandaCreateDto"></param>
        /// <response code="201">Retorna a nova comanda criada</response>
        /// <response code="400">Se a comanda for null</response>  
        /// <param name="id"></param>  
        /// 
        [HttpGet("{id}", Name = "GetComandaById")]
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
        /// /// <remarks>
        /// Modelo request:
        ///     
        ///     {
        ///        "isClose": "false"
        ///     }
        ///
        /// </remarks>
        /// <returns>Uma nova comanda criada</returns>
        /// <param name="comandaCreateDto"></param>
        /// <response code="201">Retorna a nova comanda criada</response>
        /// <response code="400">Se a comanda for null</response>     
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// <param name="id"></param>  
        [HttpPut("{id}")]
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
        /// <param name="id"></param>  
        [HttpPost("fechar/{id}")]
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

            return updateComanda(comandaModelFromRepo.Id, _mapper.Map<ComandaUpdateDTO>(comandaModelFromRepo));
        }

        /// <summary>
        /// Reseta uma comanda específica.
        /// </summary>
        /// <param name="id"></param>  
        [HttpPost("resetar/{id}")]
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

        //private IEnumerable<DescontoCreateDTO> getDesconto(IEnumerable<ComandaItens> comandaItensList)
        //{
        //    List<DescontoCreateDTO> desconto = new List<DescontoCreateDTO>();

        //    var cerveja = comandaItensList.Where(w => w.nomeItem == "Cerveja").FirstOrDefault();
        //    var conhaque = comandaItensList.Where(w => w.nomeItem == "Conhaque").FirstOrDefault();
        //    var agua = comandaItensList.Where(w => w.nomeItem == "Água").FirstOrDefault();

        //    if (cerveja != null)
        //    {
        //        var quantidadeCerveja = Convert.ToInt32(cerveja.Quantidade / 5);

        //        if(quantidadeCerveja >= 1)
        //        {
        //            desconto.Add(new DescontoCreateDTO(cerveja.IdComanda, cerveja.IdItem, cerveja.nomeItem, quantidadeCerveja, quantidadeCerveja * cerveja.ValorUnitario));
        //        }

        //        if(conhaque != null)
        //        {
        //            if(agua != null)
        //            {
        //                quantidadeCerveja = Convert.ToInt32(cerveja.Quantidade / 2);
        //                var quantidadeConhaque = Convert.ToInt32(conhaque.Quantidade / 3);

        //                if (quantidadeCerveja >= 1 && quantidadeConhaque >= 1)
        //                {
        //                    if (quantidadeCerveja <= quantidadeConhaque)
        //                    {
        //                        var quantidade = quantidadeCerveja <= agua.Quantidade ? quantidadeCerveja : agua.Quantidade;
        //                        desconto.Add(new DescontoCreateDTO(agua.IdComanda, agua.IdItem, agua.nomeItem, quantidade, quantidade * agua.ValorUnitario));
        //                    } else
        //                    {
        //                        var quantidade = quantidadeConhaque <= agua.Quantidade ? quantidadeConhaque : agua.Quantidade;
        //                        desconto.Add(new DescontoCreateDTO(agua.IdComanda, agua.IdItem, agua.nomeItem, quantidade, quantidade * agua.ValorUnitario));
        //                    }
        //                }
        //            }                   
        //        }
        //    }
        //    return desconto;
        //}

    }
}
