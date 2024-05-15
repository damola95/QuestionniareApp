using Microsoft.AspNetCore.Mvc;
using QuestionnaireApp.DTO;
using QuestionnaireApp.Interface;
using QuestionnaireApp.Model;
using System.Collections.Concurrent;
using System.ComponentModel;

namespace QuestionnaireApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionDto questionDto,  CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<Question>(false, "Invalid data", null, new List<string> { "Validation error" }));
            }

            var createdQuestion = await _questionService.CreateQuestionAsync(questionDto);
            return CreatedAtAction(nameof(GetQuestionById), new { id = createdQuestion.Id }, new ApiResponse<Question>(true, "Question created successfully.", createdQuestion));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(string id, [FromBody] QuestionDto questionDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<Question>(false, "Invalid data", null, new List<string> { "Validation error" }));
            }

            var updatedQuestion = await _questionService.UpdateQuestionAsync(id, questionDto);
            return Ok(new ApiResponse<Question>(true, "Question updated successfully.", updatedQuestion));
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await _questionService.GetQuestionsAsync();
            return Ok(questions);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(string id)
        {
            var question = await _questionService.GetQuestionsAsync();
            return question == null ? NotFound() : Ok(question);
        }

    }
}
