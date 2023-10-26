
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.DTOs;
using TestAPI.Entities;
using TestAPI.Interfaces;

namespace TestAPI.Controllers
{
    public class AccountController: BasApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context , ITokenService tokenService)
        {
            this._context = context;
            this._tokenService = tokenService;
            
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register( RegisterDto reg)
        {
            using var hcsa= new HMACSHA512();
            if( await IsUserExist(reg.UserName.ToLower()))
                return BadRequest("User already exits");

            var user= new AppUser
            {
                UserName= reg.UserName.ToLower(),
                PassworddHash= hcsa.ComputeHash(Encoding.UTF8.GetBytes(reg.Password)),
                PasswordSalt= hcsa.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                UserName= user.UserName,
                Token= _tokenService.CreateToken(user)
            };
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto log)
        {
            var user =  await _context.Users.SingleOrDefaultAsync(x=> x.UserName == log.UserName.ToLower());
            if(user == null)
                return  Unauthorized("Inavalid UserName");
            using var hasch = new HMACSHA512(user.PasswordSalt);
            var computeHash = hasch.ComputeHash(Encoding.UTF8.GetBytes(log.Password));
            for(int i=0;i< computeHash.Length;i++)
            {
                if(computeHash[i]!= user.PassworddHash[i])
                    return Unauthorized("Invalid Password");
            }
            return new UserDto
            {
                UserName= user.UserName,
                Token= _tokenService.CreateToken(user)
            };

        }
        private async Task<bool> IsUserExist(string UserName)
        {
            return await _context.Users.AnyAsync(x=> x.UserName== UserName);
        }
        
    }
}