﻿using AutoMapper;
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
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UserController(IUserRepository dbUser, IMapper mapper)
        {
            _userRepo = dbUser;
            _mapper = mapper;
            this._response = new();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _userRepo.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
        }

        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        //{
        //    bool ifUserNameUnique = _userRepo.IsUniqueUser(model.Name);
        //    if (!ifUserNameUnique)
        //    {
        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages.Add("Username already exists");
        //        return BadRequest(_response);
        //    }

        //    var user = await _userRepo.Register(model);
        //    if (user == null)
        //    {
        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages.Add("Error while registering");
        //        return BadRequest(_response);
        //    }
        //    _response.StatusCode = HttpStatusCode.OK;
        //    _response.IsSuccess = true;
        //    return Ok(_response);
        //}



        //get
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllUsers()
        {
            try
            {
                IEnumerable<User> users = await _userRepo.GetAllAsync();
                _response.Result = _mapper.Map<List<UserDTO>>(users);
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
        [HttpGet("{id:int}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetUserById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var user = await _userRepo.GetAsync(u => u.Id == id);
                if (user == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<UserDTO>(user);
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
        public async Task<ActionResult<APIResponse>> CreateUserAsync([FromBody] UserCreateDTO userCreateDTO)
        {
            try
            {
                if (await _userRepo.GetAsync(u => u.Name.ToLower() == userCreateDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "User already Exists!");
                    return BadRequest(ModelState);
                }

                if (userCreateDTO == null)
                {
                    return BadRequest(userCreateDTO);

                }

                User users = _mapper.Map<User>(userCreateDTO);
                await _userRepo.CreateAsync(users);
                _response.Result = _mapper.Map<UserDTO>(users);
                _response.StatusCode = HttpStatusCode.Created;


                return CreatedAtRoute("GetUser", new { id = users.Id }, _response);
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}",Name="DeleteUser")]
        public async Task<ActionResult<APIResponse>> DeleteUser(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var user = await _userRepo.GetAsync(u => u.Id == id);
                if (user == null)
                {
                    return NotFound();
                }
                await _userRepo.RemoveAsync(user);

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
        [HttpPut("{id:int}", Name = "UpdateUser")]
        public async Task<ActionResult<APIResponse>> UpdateUser(int id, [FromBody] UserUpdateDTO userUpdateDTO)
        {
            try
            {
                if (userUpdateDTO == null || id != userUpdateDTO.Id)
                {
                    return BadRequest();
                }
                User model = _mapper.Map<User>(userUpdateDTO);
                await _userRepo.UpdateAsync(model);
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
