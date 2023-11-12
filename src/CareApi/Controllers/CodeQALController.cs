using CareApi.Services;
using CareApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CareApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodeQALController : ControllerBase
    {
        private readonly CodeQALService _codeQALService;

        public CodeQALController(CodeQALService codeQALService)
        {
            _codeQALService = codeQALService;
        }

        [HttpGet]
        public async Task<List<CodeQAL>> GetAll() =>
            await _codeQALService.GetAllAsync();

        [HttpPost]
        public async Task<IActionResult> Post(CodeQAL codeQal)
        {
            await _codeQALService.CreateAsync(codeQal);
            return CreatedAtAction(nameof(GetAll), new { code = codeQal.Code }, codeQal);
        }
    }
}
