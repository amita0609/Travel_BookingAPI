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
    [Route("api/Flight")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IFlightRepository _dbFlight;
        private readonly IMapper _mapper;

        public FlightController(IFlightRepository dbFlight, IMapper mapper)
        {
            _dbFlight = dbFlight;
            _mapper = mapper;
            this._response = new();
        }

        //get
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetFlight()
        {
            try
            {
                IEnumerable<Flight> Flights = await _dbFlight.GetAllAsync();
                _response.Result = _mapper.Map<List<FlightDTO>>(Flights);
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
        [HttpGet("{id:int}", Name = "GetFlight")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetFlightById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var airline = await _dbFlight.GetAsync(u => u.FlightId == id);
                if (airline == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<FlightDTO>(airline);
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
        public async Task<ActionResult<APIResponse>> CreateFlightAsync([FromBody] FlightCreateDTO flightCreateDTO)
        {
            try
            {
                if (await _dbFlight.GetAsync(u => u.FlightName.ToLower() == flightCreateDTO.FlightName.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "flight already Exists!");
                    return BadRequest(ModelState);
                }

                if (flightCreateDTO == null)
                {
                    return BadRequest(flightCreateDTO);

                }



                Flight flights = _mapper.Map<Flight>(flightCreateDTO);
                await _dbFlight.CreateAsync(flights);
                _response.Result = _mapper.Map<FlightDTO>(flights);
                _response.StatusCode = HttpStatusCode.Created;


                return CreatedAtRoute("GetAirline", new { id = flights.FlightId }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteFlight")]
        public async Task<ActionResult<APIResponse>> DeleteFlight(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var flight = await _dbFlight.GetAsync(u => u.FlightId == id);
                if (flight == null)
                {
                    return NotFound();
                }
                await _dbFlight.RemoveAsync(flight);

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
        [HttpPut("{id:int}", Name = "flightUpdate")]
        public async Task<ActionResult<APIResponse>> UpdateFlight(int id, [FromBody] FlightUpdateDTO flightUpdateDTO)
        {
            try
            {
                if (flightUpdateDTO == null || id != flightUpdateDTO.FlightId)
                {
                    return BadRequest();
                }
                Flight model = _mapper.Map<Flight>(flightUpdateDTO);
                await _dbFlight.UpdateAsync(model);
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

