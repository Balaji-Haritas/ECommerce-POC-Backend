using EcommercePOC.DataAccess;
using EcommercePOC.DTO;
using EcommercePOC.Models;
using System.Security.Cryptography;
using System.Text;
using EcommercePOC.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommercePOC.Repository
{
    public class AccountRepositor:ControllerBase,IAccountRepository
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;
        public AccountRepositor(AppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<ActionResult<UserDTO>> RegisterUser(RegistrationDTO registrationDto)
        {
            if (await UserExists(registrationDto.UserName)) return BadRequest("UserName Already Exists");
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                Name = registrationDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registrationDto.Password)),
                PasswordSalt = hmac.Key,
                PhoneNumber = registrationDto.PhoneNumber,
                Email = registrationDto.Email,
                RoleId = registrationDto.RoleId
            };

            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();

            var userWithRole = await _context.AppUsers
                                     .Include(u => u.Role) 
                                     .FirstOrDefaultAsync(u => u.Id == user.Id);

            var userDto = new UserDTO
            {
                UserName = userWithRole.Name,
                Token = _tokenService.CreateToken(userWithRole),

                Role = new RoleDTO
                {
                    Id = userWithRole.Role.Id,
                    Name = userWithRole.Role.RoleName
                }
            };

            return userDto;
        }



        //};

        public async Task<ActionResult<UserDTO>> UserLogin(LoginDTO loginDto)
        {
            var user = await _context.AppUsers
                                     .Include(u => u.Role) 
                                     .FirstOrDefaultAsync(x => x.Name == loginDto.UserName.ToLower());

            if (user == null) return Unauthorized("Invalid User");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (var i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            var userDto = new UserDTO
            {
                UserName = user.Name,
                Token = _tokenService.CreateToken(user),
                Role = new RoleDTO
                {
                    Id = user.Role.Id,
                    Name = user.Role.RoleName
                }
            };

            return userDto;
        }


        public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
        {
            var usersList = await _context.AppUsers
                                          .Include(u => u.Role) // Include the Role property Include is used for Eager loading which is used load related entities as well
                                          .ToListAsync();
            return Ok(usersList);
        }


        public async Task<ActionResult<AppUser>> GetUserByThereId(int id)
        {
            var user = await _context.AppUsers
                                     .Include(u => u.Role) // Include the Role property
                                     .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound("User Not Found");
            return Ok(user);
        }


        private async Task<bool> UserExists(string name)
        {
            return await _context.AppUsers.AnyAsync(x => x.Name.ToLower() == name.ToLower());
        }
    }
}
