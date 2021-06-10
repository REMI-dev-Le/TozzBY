using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.DTO;
using System.Collections.Generic;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
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

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberupdateDTO updateDto)
    {
      var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var user = await _userRepositary.GetUserByNameAsync(username);

       _mapper.Map(updateDto,user);

       _userRepositary.Update(user);

       if(await _userRepositary.SaveAllAsync()) return NoContent();

       return BadRequest("Failed to update the user");
    }

  }
}