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
        private readonly IAccountRepository _accountService;

        public AccountController(IAccountRepository accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> RegisterUser(RegistrationDTO registrationDto)
        {
            return await _accountService.RegisterUser(registrationDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> UserLogin(LoginDTO loginDto)
        {
            return await _accountService.UserLogin(loginDto);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
        {
            return await _accountService.GetAllUsers();
        }

        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUserByThereId(int id)
        {
            return await _accountService.GetUserByThereId(id);
        }
    }
}
