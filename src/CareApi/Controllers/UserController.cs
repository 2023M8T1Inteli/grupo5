using CareApi.Models;
using CareApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CareApi.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) =>
            _userService = userService;

        [HttpGet("all")]
        public async Task<List<User>> Get() =>
            await _userService.GetManyAsync();
    
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
    }
}
