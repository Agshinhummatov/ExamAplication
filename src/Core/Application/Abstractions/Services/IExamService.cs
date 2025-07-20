using Application.DTOs.Exam;
using Application.DTOs.Lesson;
using Application.Helpers.Result;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface IExamService
    {
        Task<OperationResult> CreateExamAsync(ExamCreateAndUpdateDto dto);
        Task<OperationResult> UpdateExamAsync(int id, ExamCreateAndUpdateDto dto);
        Task<OperationResult> DeleteExamAsync(int examId);
        Task<ExamListDTO?> GetByIdAsync(int examId);
        Task<List<ExamListDTO>> GetAllAsync();

        Task<PagedResult<ExamListDTO>> GetAllPagedAsync(int page, int pageSize, string? search, int? minGrade);



    }
}
