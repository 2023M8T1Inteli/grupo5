using CareApi.Models;
using CareApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareApi.Controllers
{   
    [Authorize]
    [ApiController]
    [Route("/[controller]")]
    public class TherapyController : ControllerBase
    {
        private readonly TherapyService _therapyService;

        public TherapyController(TherapyService therapyService) =>
            _therapyService = therapyService;

        [HttpGet("all")]
        public async Task<List<Therapy>> Get() =>
            await _therapyService.GetManyAsync();

        [HttpGet("name/{name}")]
        public async Task<ActionResult<Therapy>> GetByName(string name)
        {
            var therapy = await _therapyService.GetByNameAsync(name);
            if (therapy is null) { 
                return NotFound();  
            }
            return therapy;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Therapy newTherapy)
        {
            await _therapyService.CreateOneAsync(newTherapy);
            return CreatedAtAction(nameof(Get), new { name = newTherapy.Name }, newTherapy);
        }

       

        [HttpPost("many")]
        public async Task<IActionResult> Post(List<Therapy> therapies)
        {
            await _therapyService.CreateManyAsync(therapies);
            return CreatedAtAction(nameof(Get), new object[] { therapies });
        }

        [HttpPut("name/{name}")]
        public async Task<IActionResult> UpdateByName(Therapy updatedTherapy, string name)
        {
            var therapy = await _therapyService.GetByNameAsync(name);
            if (therapy is null)
            {
                return NotFound();
            }
            updatedTherapy.Name = therapy.Name;
            if (therapy.Command!=null)
            {
               updatedTherapy.Command = therapy.Command;
            }
            await _therapyService.UpdateByNameAsync(updatedTherapy, name);
            return NoContent();
        }

        [HttpDelete("name/{name}")]
        public async Task<IActionResult> DeleteByName(string name)
        {
            var therapy = await _therapyService.GetByNameAsync(name);
            if (therapy is null) { 
                return NotFound(); 
            }
            await _therapyService.RemoveByNameAsync(name);
            return NoContent();
        }
    }
}
