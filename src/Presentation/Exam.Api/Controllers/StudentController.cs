using Application.Abstractions.Services;
using Application.DTOs.Student;
using Application.Helpers.LogMessages;

using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _studentService.GetAllAsync();
            _logger.LogInformation(StudentLogMessages.GetAllSuccess, result.Count);
            return Ok(result);
        }

        [HttpGet("Get-Filtered-Students")]
        public async Task<IActionResult> GetFilteredStudents([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchText = null)
        {
            var result = await _studentService.GetFilteredStudentsAsync(page, pageSize, searchText);
            _logger.LogInformation(StudentLogMessages.GetAllFilteredAttempt, page, pageSize, searchText);
            return Ok(result);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateStudent(StudentCreateAndUpdateDto student)
        {
            var result = await _studentService.CreateAsync(student);
            _logger.LogInformation(StudentLogMessages.CreateAttempt, result.Success, result.Message);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("Edit/{id}")] 
        public async Task<IActionResult> UpdateStudent(int id, StudentCreateAndUpdateDto student)
        {
            var result = await _studentService.UpdateAsync(id, student);
            _logger.LogInformation(StudentLogMessages.UpdateAttempt, id, result.Success, result.Message);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("Delete/{id}")] 
        public async Task<IActionResult> RemoveStudent(int id)
        {
            var result = await _studentService.DeleteAsync(id);
            _logger.LogInformation(StudentLogMessages.DeleteAttempt, id, result.Success, result.Message);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("Soft-Delete/{id}")] 
        public async Task<IActionResult> SoftDeleted(int id)
        {
            var result = await _studentService.SoftDeleteAsync(id);
            _logger.LogInformation(StudentLogMessages.SoftDeleteAttempt, id, result.Success, result.Message);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _studentService.GetByIdAsync(id);
            _logger.LogInformation(StudentLogMessages.GetByIdAttempt, id);
            return Ok(result);
        }

        [HttpGet("Search")] 
        public async Task<IActionResult> SearchStudent(string searchText)
        {
            var result = await _studentService.SearchAsync(searchText);
            _logger.LogInformation(StudentLogMessages.SearchAttempt, searchText, result.Count);
            return Ok(result);
        }

      
    }
}
