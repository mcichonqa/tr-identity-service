using AutoMapper;
using IdentityModel.Client;
using IdentityService.Contract;
using IdentityService.Entity.Models;
using IdentityService.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public IdentityController(IConfiguration configuration, IUserRepository userRepository, IMapper mapper)
        {
            _configuration = configuration;
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
                var client = new HttpClient();
                var discoveryResponse = await client.GetDiscoveryDocumentAsync(_configuration.GetValue<string>("IdentityAddress"));

                if (discoveryResponse.IsError)
                    throw new Exception(discoveryResponse.Error);

                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = discoveryResponse.TokenEndpoint,
                    ClientId = "dev-user",
                    ClientSecret = "secret",
                    Scope = "read",
                    Parameters = new Parameters()
                    {
                        { "GivenName", user.GivenName },
                        { "Surname", user.Surname },
                        { "Role", user.Role }
                    }
                });

                if (tokenResponse.IsError)
                    throw new Exception(tokenResponse.Error);

                return Ok(tokenResponse.AccessToken);
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

            await _userRepository.CreateUser(user);

            return Ok($"User {user.Username} with role {user.Role} created.");
        }
    }
}