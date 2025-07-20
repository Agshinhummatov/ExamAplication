using ExamAplication.Admin.Models.Student;
using ExamAplication.Admin.Models.Lesson;
using ExamAplication.Admin.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using ExamAplication.Admin.Services.Inerfaces;

namespace ExamAplication.Admin.Controllers
{
    public class LessonController : Controller
    {
        private readonly ILessonApiService _lessonService;

        public LessonController(ILessonApiService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task<IActionResult> Index()
        {
            var lessons = await _lessonService.GetAllAsync();
            return View(lessons);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(LessonCreate lesson)
        {
            if (!ModelState.IsValid)
                return View(lesson);

            var result = await _lessonService.CreateAsync(lesson);

            if (result.Success)
            {
                TempData["Success"] = "Lesson successfully created!";
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

            return View(lesson);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var lesson = await _lessonService.GetByIdAsync(id);
            if (lesson == null) return NotFound();
            return View(lesson);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LessonUpdate lesson)
        {
            if (!ModelState.IsValid)
                return View(lesson);

            var result = await _lessonService.UpdateAsync(lesson);

            if (result.Success)
            {
                TempData["Success"] = "Lesson successfully updated!";
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
                TempData["Error"] = "Unexpected error occurred.";
            }

            return View(lesson);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _lessonService.DeleteAsync(id);
            if (success)
                TempData["Success"] = "Lesson permanently deleted.";
            else
                TempData["Error"] = "Failed to delete the lesson.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var success = await _lessonService.SoftDeleteAsync(id);
            if (success)
                TempData["Success"] = "Lesson successfully soft-deleted.";
            else
                TempData["Error"] = "Failed to soft-delete the lesson.";

            return RedirectToAction("Index");
        }
    }
}
