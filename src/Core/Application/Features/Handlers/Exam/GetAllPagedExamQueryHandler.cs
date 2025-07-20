using Application.Abstractions.Services;
using Application.DTOs.Exam;
using Application.DTOs.Lesson;
using Application.Features.Queries.Exam;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Handlers.Exam
{
    public class GetAllPagedExamQueryHandler : IRequestHandler<ListAdvancedFilteredExamsQuery, PagedResult<ExamListDTO>>
    {
        private readonly IExamService _examService;

        public GetAllPagedExamQueryHandler(IExamService examService)
        {
            _examService = examService;
        }

        public async Task<PagedResult<ExamListDTO>> Handle(ListAdvancedFilteredExamsQuery request, CancellationToken cancellationToken)
        {
            return await _examService.GetAllPagedAsync(
                request.Page,
                request.PageSize,
                request.Search,
                request.MinGrade
            );
        }
    }
}
