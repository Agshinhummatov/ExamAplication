using Application.DTOs.Exam;
using Application.Features.Commands.Exam;
using Application.Features.Queries.Exam;
using Application.Helpers.LogMessages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ExamController> _logger;

        public ExamController(IMediator mediator, ILogger<ExamController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }



        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllExams()
        {
            var query = new GetAllExamQuery();
            var result = await _mediator.Send(query);
            _logger.LogInformation(ExamLogMessages.GetAllSuccess + " Count: {Count}", result.Count);
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetExam(int id)
        {
            var query = new GetByIdExamQuery(id);
            var result = await _mediator.Send(query);
            _logger.LogInformation(ExamLogMessages.GetByIdAttempt, id);
            return Ok(result);

        }


        [HttpGet("ListAdvancedFilter")]
        public async Task<IActionResult> ListAdvancedFilter([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchLessonCode = null, [FromQuery] int? minGrade = null)
        {
            var query = new ListAdvancedFilteredExamsQuery(page, pageSize, searchLessonCode, minGrade);
            _logger.LogInformation(ExamLogMessages.GetFilteredExam, page, pageSize, searchLessonCode, minGrade);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateExam(ExamCreateAndUpdateDto exam)
        {
            var result = await _mediator.Send(new CreateExamCommand(exam));
            _logger.LogInformation(ExamLogMessages.CreateAttempt, result.Success, result.Message);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> UpdateExam(int id, [FromBody] ExamCreateAndUpdateDto exam)
        {
            var result = await _mediator.Send(new UpdateExamCommand(id, exam));
            _logger.LogInformation(ExamLogMessages.UpdateAttempt, exam.LessonId, result.Success, result.Message);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> RemoveExam(int id)
        {
            var command = new RemoveExamCommand(id);
            var result = await _mediator.Send(command);
            _logger.LogInformation(ExamLogMessages.DeleteAttempt, id, result.Success, result.Message);
            return StatusCode(result.StatusCode, result);
        }




    }
}