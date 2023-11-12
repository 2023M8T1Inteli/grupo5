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

        [HttpGet("crefito/{crefito}")]
        public async Task<ActionResult<User>> GetByCrefito(string crefito)
        {
            var user = await _userService.GetByCrefitoAsync(crefito);
            if (user is null) { return NotFound(); };
            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Post(User newUser)
        {
            await _userService.CreateOneAsync(newUser);
            return CreatedAtAction(nameof(Get), new { name = newUser.Name }, newUser);
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

        [HttpPut("crefito/{crefito}")]
        public async Task<IActionResult> UpdateByCrefito(User updatedUser, string crefito)
        {
            var user = await _userService.GetByCrefitoAsync(crefito);
            if (user is null)
            {
                return NotFound();
            }
            updatedUser.Name = user.Name;
            await _userService.UpdateByCrefitoAsync(updatedUser, crefito);
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

        [HttpDelete("name/{crefito}")]
        public async Task<IActionResult> DeleteByCrefto(string crefito)
        {
            var user = await _userService.GetByCrefitoAsync(crefito);
            if (user is null)
            {
                return NotFound();
            }
            await _userService.RemoveByCrefito(crefito);
            return NoContent();
        }
    }
}
