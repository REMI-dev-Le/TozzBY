using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using API.Entities;
using API.Interfaces;
using API.DTO;

namespace API.Data
{
  public class UserRepositary : IUserRepositary
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public UserRepositary(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<MemberDTO> GetMemberAsync(string username)
    {
      return await _context.Users
                  .Where(x => x.UserName == username)
                  .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                  .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberDTO>> GetMemberSAsync()
    {
      return await _context.Users
                  .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                  .ToListAsync();
    }

    public async Task<AppUser> GetUserByIdAsync(int id)
    {
      return await _context.Users.FindAsync(id);
    }

    public async Task<AppUser> GetUserByNameAsync(string username)
    {
      return await _context.Users
                        .Include(p => p.Photos)
                        .SingleOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
      return await _context.Users
                     .Include(p => p.Photos)
                     .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public void Update(AppUser user)
    {
      _context.Entry(user).State = EntityState.Modified;
    }

    
  }
}