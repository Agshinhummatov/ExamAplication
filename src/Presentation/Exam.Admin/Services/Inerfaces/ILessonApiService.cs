using ExamAplication.Admin.Models.Lesson;
using ExamAplication.Admin.Models.ViewModel;
using Microsoft.AspNetCore.Http;

namespace ExamAplication.Admin.Services.Inerfaces
{
    public interface ILessonApiService
    {
        Task<List<LessonList>> GetAllAsync();
        Task<ApiResponse> CreateAsync(LessonCreate lesson);
        Task<LessonUpdate?> GetByIdAsync(int id);
        Task<ApiResponse> UpdateAsync(LessonUpdate lesson);
        Task<bool> DeleteAsync(int id);
        Task<bool> SoftDeleteAsync(int id);
    }
}
