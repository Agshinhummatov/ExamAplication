using Application.DTOs.Exam;
using Application.DTOs.Lesson;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Exam
{
    public record ListAdvancedFilteredExamsQuery(int Page, int PageSize, string? Search, int? MinGrade)
        : IRequest<PagedResult<ExamListDTO>>;
}
