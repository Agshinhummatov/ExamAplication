using ExamAplication.Admin.Models.Lesson;
using ExamAplication.Admin.Models.ViewModel;
using ExamAplication.Admin.Services.Inerfaces;
using Newtonsoft.Json;
using System.Text;

namespace ExamAplication.Admin.Services
{
    public class LessonApiService : ILessonApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LessonApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<LessonList>> GetAllAsync()
        {
            var client = _httpClientFactory.CreateClient("ExamApiClient");
            var response = await client.GetAsync("Lesson/GetAll");

            if (!response.IsSuccessStatusCode)
                return new List<LessonList>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<LessonList>>(json) ?? new List<LessonList>();
        }

        public async Task<ApiResponse> CreateAsync(LessonCreate lesson)
        {
            var client = _httpClientFactory.CreateClient("ExamApiClient");
            var json = JsonConvert.SerializeObject(lesson);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Lesson/Create", content);
            var body = await response.Content.ReadAsStringAsync();

            return new ApiResponse
            {
                Success = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode,
                Body = body
            };
        }

        public async Task<LessonUpdate?> GetByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("ExamApiClient");
            var response = await client.GetAsync($"Lesson/GetById/{id}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LessonUpdate>(json);
        }

        public async Task<ApiResponse> UpdateAsync(LessonUpdate lesson)
        {
            var client = _httpClientFactory.CreateClient("ExamApiClient");
            var json = JsonConvert.SerializeObject(lesson);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"Lesson/Edit/{lesson.Id}", content);
            var body = await response.Content.ReadAsStringAsync();

            return new ApiResponse
            {
                Success = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode,
                Body = body
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("ExamApiClient");
            var response = await client.DeleteAsync($"Lesson/Delete/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("ExamApiClient");
            var response = await client.PutAsync($"Lesson/Soft-Delete/{id}", null);
            return response.IsSuccessStatusCode;
        }
    }
}
