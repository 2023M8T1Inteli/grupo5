using CareApi.Models;
using CareApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/[controller]")]
    public class PacientController : ControllerBase
    {
        private readonly PacientService _pacientService;

        public PacientController(PacientService pacientService) =>
            _pacientService = pacientService;

        [HttpGet("all")]
        public async Task<List<Pacient>> Get() =>
            await _pacientService.GetManyAsync();

        [HttpGet("name/{name}")]
        public async Task<ActionResult<Pacient>> GetByName(string name)
        {
            var pacient = await _pacientService.GetByNameAsync(name);
            if (pacient is null) {      
                return NotFound(); 
            }
            return pacient;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Pacient newPacient)
        {
            await _pacientService.CreateOneAsync(newPacient);
            return CreatedAtAction(nameof(Get), new { name = newPacient.Name }, newPacient);
        }

        [HttpPost("many")]
        public async Task<IActionResult> Post(List<Pacient> pacients)
        {
            await _pacientService.CreateManyAsync(pacients);
            return CreatedAtAction(nameof(Get), new object[] { pacients });
        }

        [HttpPut("name/{name}")]
        public async Task<IActionResult> UpdateByName(Pacient updatedPacient, string name)
        {
            var pacient = await _pacientService.GetByNameAsync(name);
            if (pacient is null)
            {
                return NotFound();
            }
            updatedPacient.Name = pacient.Name;
            await _pacientService.UpdateByNameAsync(updatedPacient, name);
            return NoContent();
        }

        [HttpDelete("name/{name}")]
        public async Task<IActionResult> DeleteByName(string name)
        {
            var pacient = await _pacientService.GetByNameAsync(name);
            if (pacient is null)
            {
                return NotFound();
            }
            await _pacientService.RemoveByNameAsync(name);
            return NoContent();
        }
    }
}