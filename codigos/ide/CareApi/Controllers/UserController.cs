using CareApi.Dtos;
using CareApi.Models;
using CareApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace CareApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(UserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
 
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto user)
        {
            if (await _userService.CheckUserExistsByEmailAsync(user.Email)) { 
                return Conflict("User already exists with that email");
            }
            var newUser = await _userService.CreateOneAsync(user);
            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, new { id = newUser.Id, name = newUser.Name, email = newUser.Email, role = newUser.Role });
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<User>>> Get()
        {
            var users = await _userService.GetManyAsync();
            return Ok(users);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<User>> GetByName(string name)
        {
            var user = await _userService.GetByNameAsync(name);
            if (user is null) { return NotFound(); };
            return user;
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<User>> GetById(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }
            await _userService.RemoveByIdAsync(id);
            return NoContent();
        }

        [HttpPost("many")]
        public async Task<IActionResult> Post(List<User> users)
        {
            await _userService.CreateManyAsync(users);
            return CreatedAtAction(nameof(Get), new object[] { users });
        }

        [HttpPut("name/{name}")]
        public async Task<IActionResult> UpdateByName(User updatedUser, string name)
        {
            var user = await _userService.GetByNameAsync(name);
            if (user is null)
            {
                return NotFound();
            }
            updatedUser.Name = user.Name;
            await _userService.UpdateByNameAsync(updatedUser, name);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("activate")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var success = await _userService.ResetPasswordAsync(resetPasswordDto.Email, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            if (!success)
            {
                return BadRequest("Unable to reset password with provided token and email.");
            }

            return Ok("Password has been reset successfully.");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            // Validate the incomnig request
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Missing login details");
            }

            var user = await _userService.GetByEmailAsync(login.Email);
            var userHash = await _userService.GetHashedPasswordByEmailAsync(login.Email);

            if (user is null)
            {
                return BadRequest("User does no exist");
            }

            if (!_userService.VerifyPassword(login.Password, userHash))
            {
                return Unauthorized("Invalid credentials");
            }

            var token = GenerateJwtToken(user);

            //Return the token
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
