using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using TravelBookingAPI.Data;
using TravelBookingAPI.Models;
using TravelBookingAPI.Models.Dto;
using TravelBookingAPI.Repository.IRepository;

namespace TravelBookingAPI.Controllers
{
    [Route("api/Airline")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IAirlineRepository _dbAirline;
        private readonly IMapper _mapper;

        public AirlineController(IAirlineRepository dbAirline, IMapper mapper)
        {
            _dbAirline = dbAirline;
            _mapper = mapper;
            this._response = new();
        }

        //get
       
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllAirline()
        {
            try
            {
                IEnumerable<Airline> Airlines = await _dbAirline.GetAllAsync();
                _response.Result = _mapper.Map<IEnumerable<AirlineDTO>>(Airlines);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


       
        //getById
        [HttpGet("{id:int}", Name = "GetAirline")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAirlineById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var airline = await _dbAirline.GetAsync(u => u.AirlineId == id);
                if (airline == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<AirlineDTO>(airline);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }


        //post
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<APIResponse>> CreateAirlineAsync([FromBody] AirlineCreateDTO airlineCreateDTO)
        {
            try
            {
                if (await _dbAirline.GetAsync(u => u.AirlineName.ToLower() == airlineCreateDTO.AirlineName.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "User already Exists!");
                    return BadRequest(ModelState);
                }

                if (airlineCreateDTO == null)
                {
                    return BadRequest(airlineCreateDTO);

                }



                Airline airlines = _mapper.Map<Airline>(airlineCreateDTO);
                await _dbAirline.CreateAsync(airlines);
                _response.Result = _mapper.Map<AirlineDTO>(airlines);
                _response.StatusCode = HttpStatusCode.Created;


                return CreatedAtRoute("GetAirline", new { id = airlines.AirlineId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //delete
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteAirline")]
        public async Task<ActionResult<APIResponse>> DeleteAirline(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var user = await _dbAirline.GetAsync(u => u.AirlineId == id);
                if (user == null)
                {
                    return NotFound();
                }
                await _dbAirline.RemoveAsync(user);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //update
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}", Name = "UpdateAirline")]
        public async Task<ActionResult<APIResponse>> UpdateAirline(int id, [FromBody] AirlineUpdateDTO airlineUpdateDTO)
        {
            try
            {
                if (airlineUpdateDTO == null || id != airlineUpdateDTO.Id)
                {
                    return BadRequest();
                }
                Airline model = _mapper.Map<Airline>(airlineUpdateDTO);
                await _dbAirline.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }



    }
}
