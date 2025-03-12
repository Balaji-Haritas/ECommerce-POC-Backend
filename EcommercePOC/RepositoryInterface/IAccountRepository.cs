using EcommercePOC.DTO;
using EcommercePOC.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommercePOC.RepositoryInterface
{
    public interface  IAccountRepository
    {
        Task<ActionResult<UserDTO>> RegisterUser(RegistrationDTO registrationDto);
        Task<ActionResult<UserDTO>> UserLogin(LoginDTO loginDto);
        Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers();
        Task<ActionResult<AppUser>> GetUserByThereId(int id);
    }
}
