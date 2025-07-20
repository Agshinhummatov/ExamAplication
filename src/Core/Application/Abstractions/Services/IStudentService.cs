using Application.DTOs.Lesson;
using Application.DTOs.Student;
using Application.Helpers.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface IStudentService
    {
        Task<OperationResult> CreateAsync(StudentCreateAndUpdateDto Student);
        Task<List<StudentListDto>> GetAllAsync();
        Task<OperationResult> DeleteAsync(int id);
        Task<OperationResult> SoftDeleteAsync(int id);
        Task<OperationResult> UpdateAsync(int id, StudentCreateAndUpdateDto Student);
        Task<List<StudentListDto>> SearchAsync(string? searchText);
        Task<StudentListDto> GetByIdAsync(int id);

        Task<PagedResult<StudentListDto>> GetFilteredStudentsAsync(int page, int pageSize, string? searchText);

    }
}
