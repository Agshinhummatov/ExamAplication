using ExamAplication.Admin.Models.Exam;
using ExamAplication.Admin.Models.Lesson;
using ExamAplication.Admin.Models.Student;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ExamAplication.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("ExamApiClient");

            var lessonResponse = await client.GetAsync("Lesson/GetAll");
            var studentResponse = await client.GetAsync("Student/GetAll");
            var examResponse = await client.GetAsync("Exam/GetAll");

            int lessonCount = 0;
            int studentCount = 0;
            int examCount = 0;

            if (lessonResponse.IsSuccessStatusCode)
            {
                var lessonJson = await lessonResponse.Content.ReadAsStringAsync();
                var lessons = JsonConvert.DeserializeObject<List<LessonList>>(lessonJson);
                lessonCount = lessons.Count;
            }

            if (studentResponse.IsSuccessStatusCode)
            {
                var studentJson = await studentResponse.Content.ReadAsStringAsync();
                var students = JsonConvert.DeserializeObject<List<StudentList>>(studentJson);
                studentCount = students.Count;
            }

            if (examResponse.IsSuccessStatusCode)
            {
                var examJson = await examResponse.Content.ReadAsStringAsync();
                var exams = JsonConvert.DeserializeObject<List<ExamList>>(examJson);
                examCount = exams.Count;
            }

            ViewBag.LessonCount = lessonCount;
            ViewBag.StudentCount = studentCount;
            ViewBag.ExamCount = examCount;

            return View();
        }
    }

}
