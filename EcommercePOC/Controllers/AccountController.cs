using System.Security.Cryptography;
using System.Text;
using EcommercePOC.DataAccess;
using EcommercePOC.DTO;
using EcommercePOC.Models;
using EcommercePOC.RepositoryInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommercePOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        private readonly ITokenService _tokenService;

        public AccountController(AppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> RegisterUser(RegistrationDTO registrationDto)
        {
            if (await UserExists(registrationDto.UserName)) return BadRequest("UserName Already Exists");
            using var hmac = new HMACSHA512();// used to encrypt the password in the form of hash
                                              // using helps in the direct automatic moveing to the garbage collector when it is no longer used

            var user = new AppUser
            {
                Name = registrationDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registrationDto.Password)),
                PasswordSalt = hmac.Key,
                PhoneNumber = registrationDto.PhoneNumber,
                Email = registrationDto.Email,
            };

            _context.AppUsers.Add(user);

            await _context.SaveChangesAsync();

            return new UserDTO
            {
                UserName = user.Name,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string name)
        {
            return await _context.AppUsers.AnyAsync(x => x.Name.ToLower() == name.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> UserLogin(LoginDTO loginDto)
        {
            var user = await _context.AppUsers.FirstOrDefaultAsync(x => x.Name == loginDto.UserName.ToLower());

            if (user == null) return Unauthorized("Invalid User");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (var i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return new UserDTO
            {
                UserName = user.Name,
                Token = _tokenService.CreateToken(user)
            };

        }

        [AllowAnonymous] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
        {
            var usersList = await _context.AppUsers.ToListAsync();

            return usersList;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUserByThereId(int id)
        {
            var user = await _context.AppUsers.FindAsync(id);

            if (user == null)
                return NotFound();
            return user;
        }
    }
}
