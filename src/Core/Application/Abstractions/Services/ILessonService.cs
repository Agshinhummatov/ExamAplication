using Application.DTOs.Lesson;
using Application.Helpers.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface ILessonService
    {
        Task<OperationResult> CreateAsync(LessonCreateAndUpdateDto lesson);
        Task<List<LessonListDto>> GetAllAsync();
        Task<OperationResult> DeleteAsync(int id);
        Task<OperationResult> SoftDeleteAsync(int id);
        Task<OperationResult> UpdateAsync(int id, LessonCreateAndUpdateDto lesson);
        Task<List<LessonListDto>> SearchAsync(string? searchText);
        Task<LessonListDto> GetByIdAsync(int id);

        Task<PagedResult<LessonListDto>> GetFilteredLessonsAsync(int page, int pageSize, string? searchText);

    }
}
