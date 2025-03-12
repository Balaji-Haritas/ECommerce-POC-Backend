using System.ComponentModel.DataAnnotations;

namespace EcommercePOC.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        public required string Email { get; set; }
        public required string Name { get; set; }

        public required byte[] PasswordHash { get; set; }
        //because now we are using the IdentityUser which is having password salting by default

        public required byte[] PasswordSalt { get; set; } // using of password salt helps in the preventing the duplication passwords

        [MaxLength(10)]
        public required string PhoneNumber { get; set; }

        // Foreign key for Role
        public int RoleId { get; set; }

        // Navigation property for Role
        public Role Role { get; set; }

    }
}
