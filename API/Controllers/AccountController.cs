using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using API.DTO;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;
namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _TokenService;
        public AccountController(DataContext context, ITokenService TokenService)
        {
            _TokenService = TokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDto registerDto)
        {
            if (await (UserExists(registerDto.Username))) return BadRequest("Username is Taken");
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

              return new UserDTO
            {
                Username = user.UserName,
                Token = _TokenService.CreateToken(user)
            };
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO logindto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == logindto.Username);

            if (user == null) return Unauthorized("Inavlid Username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(logindto.Password));

            for (int i = 0; i < ComputedHash.Length; i++)
            {
                if (ComputedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return new UserDTO
            {
                Username = user.UserName,
                Token = _TokenService.CreateToken(user)
            };

        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}