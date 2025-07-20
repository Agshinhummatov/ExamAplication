using ExamAplication.Admin.Models.Exam;
using ExamAplication.Admin.Models.Lesson;
using ExamAplication.Admin.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExamAplication.Admin.Controllers
{
    public class ExamController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ExamController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("ExamApiClient");
            var response = await client.GetAsync("Exam/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var examsJson = await response.Content.ReadAsStringAsync();
                var exams = JsonConvert.DeserializeObject<List<ExamList>>(examsJson);

                if (exams != null)
                {
                    return View(exams);
                }
            }

            return View("Error");
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var client = _httpClientFactory.CreateClient("ExamApiClient");
            var response = await client.GetAsync("Lesson/GetAll");

            List<LessonList> lessons = new List<LessonList>();

            if (response.IsSuccessStatusCode)
            {
                var lessonsJson = await response.Content.ReadAsStringAsync();
                lessons = JsonConvert.DeserializeObject<List<LessonList>>(lessonsJson);
            }


            ViewBag.Lessons = new SelectList(lessons.Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.LessonName
            }), "Value", "Text");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExamCreate exam)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient("ExamApiClient");
                var json = JsonConvert.SerializeObject(exam);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("Exam/Create", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Lesson successfully created!";
                    return RedirectToAction("Index");
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(responseBody);

                    if (errorResponse?.Errors != null)
                    {
                        var allErrors = new List<string>();

                        foreach (var error in errorResponse.Errors)
                        {
                            foreach (var message in error.Value)
                            {
                                ModelState.AddModelError(error.Key, message);
                                allErrors.Add(message);
                            }
                        }

                        TempData["ErrorList"] = JsonConvert.SerializeObject(allErrors);
                    }
                }
                else
                {
                    TempData["Error"] = "Unexpected error occurred. Please try again.";
                }
            }
            await PopulateLessonsAsync();
            return View(exam);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("ExamApiClient");

           
            var responseExam = await client.GetAsync($"Exam/GetById/{id}");

            if (!responseExam.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var examJson = await responseExam.Content.ReadAsStringAsync();
            var exam = JsonConvert.DeserializeObject<ExamUpdate>(examJson);

           
            var responseLessons = await client.GetAsync("Lesson/GetAll");

            List<LessonList> lessons = new List<LessonList>();

            if (responseLessons.IsSuccessStatusCode)
            {
                var lessonsJson = await responseLessons.Content.ReadAsStringAsync();
                lessons = JsonConvert.DeserializeObject<List<LessonList>>(lessonsJson);
            }


            ViewBag.Lessons = new SelectList(lessons?.Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.LessonName
            }), "Value", "Text");

            return View(exam);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ExamUpdate exam)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient("ExamApiClient");
                var json = JsonConvert.SerializeObject(exam);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"Exam/Edit/{exam.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Lesson successfully updated!";
                    return RedirectToAction("Index");
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(body);

                    if (errorResponse?.Errors != null)
                    {
                        var allErrors = new List<string>();

                        foreach (var error in errorResponse.Errors)
                        {
                            foreach (var message in error.Value)
                            {
                                ModelState.AddModelError(error.Key, message);
                                allErrors.Add(message);
                            }
                        }

                        TempData["ErrorList"] = JsonConvert.SerializeObject(allErrors);
                    }
                }
                else
                {
                    TempData["Error"] = "Unexpected error occurred.";
                }
            }

            await PopulateLessonsAsync();
            return View(exam);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("ExamApiClient");

            var command = new { Id = id };
            var json = JsonConvert.SerializeObject(command);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.DeleteAsync($"Exam/Delete/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Error");
        }



        private async Task PopulateLessonsAsync()
        {
            var client = _httpClientFactory.CreateClient("ExamApiClient");
            var response = await client.GetAsync("Lesson/GetAll");

            List<LessonList> lessons = new List<LessonList>();
            if (response.IsSuccessStatusCode)
            {
                var lessonsJson = await response.Content.ReadAsStringAsync();
                lessons = JsonConvert.DeserializeObject<List<LessonList>>(lessonsJson);
            }

            ViewBag.Lessons = new SelectList(lessons.Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.LessonName
            }), "Value", "Text");
        }



    }
}
