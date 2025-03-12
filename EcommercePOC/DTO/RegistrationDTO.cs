namespace EcommercePOC.DTO
{
    public class RegistrationDTO
    {
        public required string Email { get; set; }

        public required string UserName { get; set; }

        public required string Password { get; set; }

        public required string PhoneNumber { get; set; }
        public int RoleId { get; set; }
    }
}
