using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBarDg.Data;
using TestBarDg.DTOs;

namespace TestBarDg.Controllers
{
    [Route("api/comandas")]
    [ApiController]
    public class ComandasController : ControllerBase
    {
        private readonly IBarDGRepo _repository;
        private readonly IMapper _mapper;

        public ComandasController(IBarDGRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ComandaReadDTO>> GetAllComandas()
        {
            var comandas = _repository.GetAllComandas();

            return Ok(_mapper.Map<IEnumerable<ComandaReadDTO>>(comandas));
        }

        [HttpGet("{id}")]
        public ActionResult<ComandaReadDTO> GetComandaById(int id)
        {
            var comanda = _repository.GetComandaById(id);

            if (comanda != null)
            {
                return Ok(_mapper.Map<ComandaReadDTO>(comanda));
            }
            return NotFound();

        }

        [HttpPut("{id}")]
        public ActionResult updateComanda(int id, ComandaUpdateDTO comandaUpdateDTO)
        {
            var comandaModelFromRepo = _repository.GetComandaById(id);
            if(comandaModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(comandaUpdateDTO, comandaModelFromRepo);

            _repository.updateComanda(comandaModelFromRepo);

            _repository.saveChanges();

            return NoContent();

        }

        [HttpPatch("{id}")]
        public ActionResult partialComandaUpdate(int id, JsonPatchDocument<ComandaUpdateDTO> patchDoc)
        {
            var comandaModelFromRepo = _repository.GetComandaById(id);
            if (comandaModelFromRepo == null)
            {
                return NotFound();
            }

            var comandaToPatch = _mapper.Map<ComandaUpdateDTO>(comandaModelFromRepo);
            patchDoc.ApplyTo(comandaToPatch, ModelState);
            
            if (!TryValidateModel(comandaToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(comandaToPatch, comandaModelFromRepo);

            _repository.updateComanda(comandaModelFromRepo);

            _repository.saveChanges();

            return NoContent();
        }

    }
}
