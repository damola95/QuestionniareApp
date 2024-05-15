using Microsoft.AspNetCore.Mvc;
using QuestionnaireApp.DTO;
using QuestionnaireApp.Interface;
using QuestionnaireApp.Model;
using QuestionnaireApp.Service;

namespace QuestionnaireApp.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProgramsApplicationController : ControllerBase
    {

        private readonly IProgramApplicationService _programAppService;

        public ProgramsApplicationController(IProgramApplicationService programAppService)
        {
            _programAppService = programAppService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] Programs programAppModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<Programs>(false, "Invalid data", null, new List<string> { "Validation error" }));
            }

            var createdProgram = await _programAppService.AddPorgramApplicationAsync(programAppModel);
            return CreatedAtAction(nameof(GetQuestionById), new { id = createdProgram.Id }, new ApiResponse<Programs>(true, "Question created successfully.", createdProgram));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(string id)
        {
            var program = await _programAppService.GetQuestionsAsync();
            return program == null ? NotFound() : Ok(program);
        }
    }
}
