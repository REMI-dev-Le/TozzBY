using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.DTO;
using System.Collections.Generic;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
  [Authorize]
  public class UsersController : BaseApiController
  {

    private readonly IUserRepositary _userRepositary;
    private readonly IMapper _mapper;

    public UsersController(IUserRepositary userRepositary, IMapper mapper)
    {
      _mapper = mapper;
      _userRepositary = userRepositary;

    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUser()
    {
      var users = await _userRepositary.GetMemberSAsync();
      return Ok(users);
    }
    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDTO>> GetUser(string username)
    {
      return await _userRepositary.GetMemberAsync(username);
     
    }

  }
}