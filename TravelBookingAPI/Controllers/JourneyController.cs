using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TravelBookingAPI.Data;
using TravelBookingAPI.Models;
using TravelBookingAPI.Models.Dto;
using TravelBookingAPI.Repository.IRepository;


namespace TravelBookingAPI.Controllers
{
    [Route("api/Journey")]
    [ApiController]
    public class JourneyController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IJourneyRepository _dbJourney;
        private readonly IMapper _mapper;

        public JourneyController(IJourneyRepository dbJourney, IMapper mapper)
        {
            _dbJourney = dbJourney;
            _mapper = mapper;
            this._response = new();
        }

        //get
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetJourney()
        {
            try
            {
                IEnumerable<Journey> journey = await _dbJourney.GetAllAsync();
                _response.Result = _mapper.Map<List<JourneyDTO>>(journey);
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
        [HttpGet("{id:int}", Name = "GetJourney")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetJourneyById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var journey = await _dbJourney.GetAsync(u => u.JourneyId == id);
                if (journey == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<JourneyDTO>(journey);
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
        public async Task<ActionResult<APIResponse>> CreateJourneyAsync([FromBody] JourneyCreateDTO journeyCreateDTO)
        {
            try
            {
                //if (await _dbJourney.GetAsync(u => u.JourneyName.ToLower() == journeyCreateDTO.FlightName.ToLower()) != null)
                //{
                //    ModelState.AddModelError("ErrorMessages", "Journey already Exists!");
                //    return BadRequest(ModelState);
                //}

                if (journeyCreateDTO == null)
                {
                    return BadRequest(journeyCreateDTO);

                }



                Journey journey = _mapper.Map<Journey>(journeyCreateDTO);
                await _dbJourney.CreateAsync(journey);
                _response.Result = _mapper.Map<JourneyDTO>(journey);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetJourney", new { id = journey.JourneyId }, _response);
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
        [Authorize(Roles ="admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteJourney")]
        public async Task<ActionResult<APIResponse>> DeleteJourney(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var journey = await _dbJourney.GetAsync(u => u.JourneyId == id);
                if (journey == null)
                {
                    return NotFound();
                }
                await _dbJourney.RemoveAsync(journey);

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
        [HttpPut("{id:int}", Name = "UpdateJourney")]
        public async Task<ActionResult<APIResponse>> UpdateJourney(int id, [FromBody] JourneyUpdateDTO journeyUpdateDTO)
        {
            try
            {
                if (journeyUpdateDTO == null || id != journeyUpdateDTO.FlightId)
                {
                    return BadRequest();
                }
                Journey model = _mapper.Map<Journey>(journeyUpdateDTO);
                await _dbJourney.UpdateAsync(model);
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

