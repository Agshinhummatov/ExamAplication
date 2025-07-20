using Application.Abstractions.Services;
using Application.DTOs.Lesson;
using Application.Features.Commands.Lesson;
using Application.Features.Queries.Lesson;
using Application.Helpers.LogMessages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistence.Services;

namespace Exam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {

        private readonly ILogger<LessonController> _logger;
        private readonly IMediator _mediator;

        public LessonController( ILogger<LessonController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllLessons()
        {

            var result = await _mediator.Send(new GetAllLessonsQuery());
            _logger.LogInformation(LessonLogMessages.GetAllSuccess + " Count: {Count}", result.Count);
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetLessonByIdQuery(id));
            _logger.LogInformation(LessonLogMessages.GetByIdAttempt, id);
            return Ok(result);
        }

        [HttpGet("GetFilteredLessons")]
        public async Task<IActionResult> GetFilteredLessons([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchLessonCode = null)
        {
            var query = new GetFilteredLessonsQuery(page, pageSize, searchLessonCode);
            var result = await _mediator.Send(query);
            _logger.LogInformation(LessonLogMessages.GetFilteredLessons, page, pageSize, searchLessonCode);
            return Ok(result);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchLesson([FromQuery] string searchLessonCodeText)
        {
            var result = await _mediator.Send(new SearchLessonsQuery(searchLessonCodeText));
            _logger.LogInformation(LessonLogMessages.SearchAttempt, searchLessonCodeText, result.Count);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateLesson(LessonCreateAndUpdateDto lesson)
        {
            var result = await _mediator.Send(new CreateLessonCommand(lesson));
            _logger.LogInformation(LessonLogMessages.CreateAttempt, result.Success, result.Message);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> UpdateLesson(int id, [FromBody] LessonCreateAndUpdateDto lesson)
        {
            var result = await _mediator.Send(new UpdateLessonCommand(id, lesson));
            _logger.LogInformation(LessonLogMessages.UpdateAttempt, id, result.Success, result.Message);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("Soft-Delete/{id}")]
        public async Task<IActionResult> SoftDeleted(int id)
        {
            var result = await _mediator.Send(new SoftDeleteLessonCommand(id));
            _logger.LogInformation(LessonLogMessages.SoftDeleteAttempt, id, result.Success, result.Message);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> RemoveLesson(int id)
        {
            var result = await _mediator.Send(new RemoveLessonCommand(id));
            _logger.LogInformation(LessonLogMessages.DeleteAttempt, id, result.Success, result.Message);
            return StatusCode(result.StatusCode, result);
        }


    }
}
