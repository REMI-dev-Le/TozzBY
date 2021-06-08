using API.DTO;
using API.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace API.Interfaces
{
    public interface IUserRepositary
    {
         void Update(AppUser user);
         Task<bool> SaveAllAsync();
         Task<IEnumerable<AppUser>> GetUsersAsync();
         Task<AppUser> GetUserByIdAsync(int id);
         Task<AppUser> GetUserByNameAsync(string username);

         Task<IEnumerable<MemberDTO>> GetMemberSAsync();
         Task<MemberDTO> GetMemberAsync(string username);
    }
}