using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using Microsoft.EntityFrameworkCore;
using DatingApp.API.Models;
using DatingApp.API.Dtos;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;


namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private IAuthRepository _repo;
        private IConfiguration _configuration;
        public AuthController(IAuthRepository repo, IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
            
        }
    
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForRegisterDto user_dto)
        {
        

        // Validation

        var username_lower = user_dto.Username.ToLower();
        var password = user_dto.Password;
        if(await _repo.UserExists(username_lower))
        {
            return BadRequest("Error");
        }

        User user = new User();
        user.Username = username_lower;

    
       var created_user = await  _repo.Register(user, password);
       return StatusCode(201);


        }
    [HttpPost("login")]
    
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        string user_name = userLoginDto.Username.ToLower();
        string password = userLoginDto.Password;

        User userFromRepo = await this._repo.Login(user_name, password);
        
        if(userFromRepo == null )
        {
            return Unauthorized();
        }

        var claims = new []
        {
            new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
            new Claim(ClaimTypes.Name, userFromRepo.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.
        GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Ok(new {
            token = tokenHandler.WriteToken(token)
        });
    }

    }
}