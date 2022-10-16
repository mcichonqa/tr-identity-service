using AutoMapper;
using IdentityService.Contract;
using IdentityService.Entity.Models;
using IdentityService.Helpers;
using IdentityService.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace IdentityService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public IdentityController(IConfiguration config, IUserRepository userRepository, IMapper mapper)
        {
            _config = config;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            User user = await _userRepository.GetUser(userLogin.Username);

            if (user != null)
            {
                string token = AuthenticateHelper.GenerateToken(_config, user);

                return Ok(token);
            }

            return NotFound($"User named {userLogin.Username} not found.");
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationInfoDto registrationInfo)
        {
            var userForValidation = await _userRepository.GetUser(registrationInfo.Username);

            if (userForValidation != null)
                return Conflict($"User named {registrationInfo.Username} already exists.");

            var user = _mapper.Map<User>(registrationInfo);
            user.Role = UserType.Customer.ToString();

            await _userRepository.CreateUser(user);

            return Ok($"User {user.Username} created.");
        }
    }
}