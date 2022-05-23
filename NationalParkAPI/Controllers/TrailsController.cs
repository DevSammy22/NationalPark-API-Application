using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalPark.Data.Repository.Interfaces;
using NationalPark.Dto.Request;
using NationalPark.Models;
using System.Collections.Generic;

namespace NationalParkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class TrailsController : ControllerBase
    {
        private readonly ITrailRepository _trailRepository;
        private readonly IMapper _mapper;

        public TrailsController(ITrailRepository trailRepository, IMapper mapper)
        {
            _trailRepository = trailRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get the list of trails
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllTrails")]
        [ProducesResponseType(200, Type = typeof(List<TrailRequestDto>))]
        public IActionResult GetAllTrails()
        {
            var ListOfTrails = _trailRepository.GetAllTrails();
            var trailDto = new List<TrailRequestDto>();
            foreach (var trail in ListOfTrails)
            {
                trailDto.Add(_mapper.Map<TrailRequestDto>(trail));
            }
            return Ok(trailDto);
        }

        /// <summary>
        /// Get individual trail
        /// </summary>
        /// <param name="trailId"></param>
        /// <returns></returns>
        [HttpGet("{trailId:int}", Name = "GetTrailById")]
        [ProducesResponseType(200, Type = typeof(TrailRequestDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrailById(int trailId)
        {
            var trail = _trailRepository.GetTrailById(trailId);
            if (trail == null)
            {
                return NotFound();
            }
            var trailDto = _mapper.Map<TrailRequestDto>(trail);
            return  Ok(trailDto);
        }

        [HttpPost]
        [Route("CreateTrail")]
        [ProducesResponseType(201, Type = typeof(TrailCreateDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTrail([FromBody] TrailCreateDto trailCreateDto)
        {
            if (trailCreateDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_trailRepository.TrailExists(trailCreateDto.Name))
            {
                ModelState.AddModelError("", "Trail Exists");
                return StatusCode(400, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var trail = _mapper.Map<Trail>(trailCreateDto);
            if (!_trailRepository.CreateTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong while saving the record {trail.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetTrailById", new {trailId = trail.Id}, trail);
        }

        [HttpPatch]
        [Route("UpdateTrailById")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrailById(int trailId, [FromBody] TrailUpdateDto trailUpdatetDto)
        {
            if (trailUpdatetDto == null || trailId != trailUpdatetDto.Id)
            {
                return BadRequest(ModelState);
            }

            var trail = _mapper.Map<Trail>(trailUpdatetDto);
            if (!_trailRepository.UpdateTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {trail.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteTrailById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrailById(int trailId)
        {
            if (!_trailRepository.TrailExists(trailId))
            {
                return NotFound();
            }

            var trail = _trailRepository.GetTrailById(trailId);
            if (!_trailRepository.DeleteTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {trail.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
