using ExamAplication.Admin.Models.Student;
using ExamAplication.Admin.Models.ViewModel;

namespace ExamAplication.Admin.Services.Inerfaces
{
    public interface IStudentApiService
    {
        Task<List<StudentList>> GetAllAsync();
        Task<StudentUpdate?> GetByIdAsync(int id);
        Task<ApiResponse> CreateAsync(StudentCreate student);
        Task<ApiResponse> UpdateAsync(StudentUpdate student);
        Task<bool> DeleteAsync(int id);
        Task<bool> SoftDeleteAsync(int id);
    }
}
