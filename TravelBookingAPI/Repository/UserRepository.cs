using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using TravelBookingAPI.Data;
using TravelBookingAPI.Models;
using TravelBookingAPI.Models.Dto;
using TravelBookingAPI.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
  


namespace TravelBookingAPI.Repository 
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
      
        private string secretKey;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext db, IConfiguration configuration,
           IMapper mapper) : base(db)
        {
            _db = db;
            _mapper = mapper;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");   

        }

        //public bool IsUniqueUser(string name)
        //{
        //    var user = _db.Users.FirstOrDefault(x => x.Name == name);
        //    if (user == null)
        //    {
        //        return true;
        //    }
        //    return false;
        //}



        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.Users
                .FirstOrDefault(u => u.Name.ToLower() == loginRequestDTO.Name.ToLower() 
                && u.Password==loginRequestDTO.Password);

            if (user == null )
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
               
            }

            //if user was found generate JWT Token
           
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                 User = _mapper.Map<UserDTO>(user),
                

            };
            return loginResponseDTO;
        }

        //public async Task<User> Register(RegistrationRequestDTO registerationRequestDTO)
        //{
        //     User user = new()
        //    {
        //        Name = registerationRequestDTO.Name,
        //        Email = registerationRequestDTO.Email,
        //        Password = registerationRequestDTO.Password,
        //        Role = registerationRequestDTO.Role
        //    };

        //        _db.Users.Add(user);
        //      await  _db.SaveChangesAsync();
        //        user.Password = "";
        //        return user;
        //}
    

         public async Task<User> UpdateAsync(User user)
         {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return user;
         }

      
    }
}
