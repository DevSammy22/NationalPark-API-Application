using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalPark.Data.Repository.Interfaces;
using NationalPark.Dto;
using NationalPark.Models;
using System.Collections.Generic;

namespace NationalParkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ParkController : ControllerBase //you can change this to controller
    {
        private readonly IParkRepository _parkRepository;
        private readonly IMapper _mapper;

        public ParkController(IParkRepository parkRepository, IMapper mapper)
        {
            _parkRepository = parkRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get the list of parks.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllParks")]
        [ProducesResponseType(200, Type = typeof(List<ParkRequestDto>))]
        public IActionResult GetAllParks()
        {
            var parkList = _parkRepository.GetParks();
            var parkRequestDto = new List<ParkRequestDto>();
            foreach (var obj in parkList)
            {
                parkRequestDto.Add(_mapper.Map<ParkRequestDto>(obj));
            }
            return Ok(parkRequestDto);
        }

        /// <summary>
        /// Get the individual park by Id
        /// </summary>
        /// <param name="parkId"></param>
        /// <returns></returns>
        [HttpGet("{ParkId:int}", Name = "GetParkById")]
        [ProducesResponseType(200, Type = typeof(ParkRequestDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetParkById(int parkId)
        {
            var park = _parkRepository.GetPark(parkId);
            if (park == null)
            {
                return NotFound();
            }
            var parkRequestDto = _mapper.Map<ParkRequestDto>(park);
            return Ok(parkRequestDto);
        }

        [HttpPost]
        [Route("CreatePark")]
        [ProducesResponseType(201, Type = typeof(ParkRequestDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreatePark([FromBody] ParkRequestDto parkRequestDto)
        {
            if (parkRequestDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_parkRepository.ParkExists(parkRequestDto.Name))
            {
                ModelState.AddModelError("", "The park you are tyring to add already exists!");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var park = _mapper.Map<Park>(parkRequestDto);
            if (!_parkRepository.CreatePark(park))
            {
                ModelState.AddModelError("", $"Something went wrong while saving the record {park.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetParkById", new {parkId = park.Id}, park);
        }

        [HttpPatch]
        [Route("UpdateParkById")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateParkById(int parkId, [FromBody] ParkRequestDto parkRequestDto)
        {
            if (parkRequestDto == null || parkId != parkRequestDto.Id)
            {
                return BadRequest(ModelState);
            }

            var park = _mapper.Map<Park>(parkRequestDto);
            if (!_parkRepository.UpdatePark(park))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {park.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        
        [HttpDelete]
        [Route("DeleteParkById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteParkById(int parkId)
        {
            if (!_parkRepository.ParkExists(parkId))
            {
                return NotFound();
            }

            var park = _parkRepository.GetPark(parkId);
            if (!_parkRepository.DeletePark(park))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {park.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
