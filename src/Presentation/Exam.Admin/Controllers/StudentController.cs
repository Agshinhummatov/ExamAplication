using ExamAplication.Admin.Models.Student;
using ExamAplication.Admin.Response;
using ExamAplication.Admin.Services;
using ExamAplication.Admin.Services.Inerfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;
using System.Text;

namespace Exam.Admin.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentApiService _studentService;
      

        public StudentController(IStudentApiService studentService)
        {
            _studentService = studentService;

        }


        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllAsync();

            var classData = students
                .GroupBy(s => s.Class)
                .Select(g => new { Class = g.Key, Count = g.Count() })
                .ToList();

            ViewBag.StudentClassStats = JsonConvert.SerializeObject(classData);
            return View(students);
        }

        public IActionResult Create() => View();

     
        [HttpPost]
        public async Task<IActionResult> Create(StudentCreate student)
        {
            if (!ModelState.IsValid)
                return View(student);

            var result = await _studentService.CreateAsync(student);


            if (result.Success)
            {
                TempData["Success"] = "Student successfully created!";
                return RedirectToAction("Index");
            }

            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(result.Body!);
                if (errorResponse?.Errors != null)
                {
                    var allErrors = new List<string>();

                    foreach (var error in errorResponse.Errors)
                        foreach (var msg in error.Value)
                        {
                            ModelState.AddModelError(error.Key, msg);
                            allErrors.Add(msg);
                        }

                    TempData["ErrorList"] = JsonConvert.SerializeObject(allErrors);
                }
            }
            else
            {
                TempData["Error"] = "Unexpected error occurred. Please try again.";
            }




            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null) return NotFound();

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentUpdate student)
        {
            if (!ModelState.IsValid)
                return View(student);

            var result = await _studentService.UpdateAsync(student);

            if (result.Success)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Student not updated");
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _studentService.DeleteAsync(id);
            return success ? RedirectToAction("Index") : View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var success = await _studentService.SoftDeleteAsync(id);
            return success ? RedirectToAction("Index") : View("Error");
        }
    }

}
