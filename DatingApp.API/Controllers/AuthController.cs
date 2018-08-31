using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using Microsoft.EntityFrameworkCore;
using DatingApp.API.Models;
using DatingApp.API.Dtos;


namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
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
    
    
    }


}