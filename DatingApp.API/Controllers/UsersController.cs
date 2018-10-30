using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        public IDatingRepository _repo;
        public IMapper _mapper; 
        
        public UsersController(IDatingRepository _repo, IMapper mapper)
        {
            this._repo = _repo;
            this._mapper = mapper;
        }
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await this._repo.GetUsers();
        var userToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
        return Ok(userToReturn);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        Console.WriteLine("Current ID: {0}\n", id);
        var user = await this._repo.GetUser(id);
        var userToReturn = this._mapper.Map<UserForDEtailedDto>(user);
        return Ok(userToReturn);
    }



    }
}