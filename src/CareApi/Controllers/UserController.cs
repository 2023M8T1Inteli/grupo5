using CareApi.Dtos;
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

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="createUserDto">The user data transfer object.</param>
        /// <returns>A newly created user.</returns>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto user)
        {
            var newUser = await _userService.CreateOneAsync(user);
            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, new { id = newUser.Id, name = newUser.Name, email = newUser.Email, role = newUser.Role });
        }

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

        [HttpDelete("name/{name}")]
        public async Task<IActionResult> DeleteByName(string name)
        {
            var user = await _userService.GetByNameAsync(name);
            if (user is null)
            {
                return NotFound();
            }
            await _userService.RemoveByNameAsync(name);
            return NoContent();
        }
    }
}
