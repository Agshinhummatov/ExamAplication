using ExamAplication.Admin.Models.Student;
using ExamAplication.Admin.Models.ViewModel;
using ExamAplication.Admin.Services.Inerfaces;
using Newtonsoft.Json;
using System.Text;

namespace ExamAplication.Admin.Services
{
    public class StudentApiService : IStudentApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StudentApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient CreateClient() => _httpClientFactory.CreateClient("ExamApiClient");

        public async Task<List<StudentList>> GetAllAsync()
        {
            var response = await CreateClient().GetAsync("Student/GetAll");
            if (!response.IsSuccessStatusCode) return new();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<StudentList>>(json) ?? new();
        }

        public async Task<StudentUpdate?> GetByIdAsync(int id)
        {
            var response = await CreateClient().GetAsync($"Student/GetById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<StudentUpdate>(json);
        }

        public async Task<ApiResponse> CreateAsync(StudentCreate student)
        {
            var json = JsonConvert.SerializeObject(student);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await CreateClient().PostAsync("Student/Create", content);
            var body = await response.Content.ReadAsStringAsync();

            return new ApiResponse
            {
                Success = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode,
                Body = body
            };
        }

        public async Task<ApiResponse> UpdateAsync(StudentUpdate student)
        {
            var json = JsonConvert.SerializeObject(student);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await CreateClient().PutAsync($"Student/Edit/{student.Id}", content);
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
            var response = await CreateClient().DeleteAsync($"Student/Delete/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var response = await CreateClient().PutAsync($"Student/Soft-Delete/{id}", null);
            return response.IsSuccessStatusCode;
        }
    }
}
