
namespace IdentityService.Contract
{
    public class RegistrationInfoDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string GivenName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
}