using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.DTOs.Exam;
using Application.Features.Queries.Exam;
using Application.Helpers.Result;
using AutoMapper;
using MediatR;

namespace Application.Features.Handlers.Exam
{
    public class GetExamByIdQueryHandler : IRequestHandler<GetByIdExamQuery, ExamListDTO>
    {
        private readonly IExamService _examService;

        public GetExamByIdQueryHandler(IExamService examService)
        {
            _examService = examService;
        }

        public async Task<ExamListDTO> Handle(GetByIdExamQuery request, CancellationToken cancellationToken)
        {
            return await _examService.GetByIdAsync(request.Id);
        }
    }
}