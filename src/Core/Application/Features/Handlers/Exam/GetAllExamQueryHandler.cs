using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.DTOs.Exam;
using Application.Features.Queries.Exam;
using Application.Helpers.Result;
using AutoMapper;
using MediatR;

namespace Application.Features.Handlers.Exam
{
    public class GetAllExamQueryHandler : IRequestHandler<GetAllExamQuery, List<ExamListDTO>>
    {
        private readonly IExamService _examService;

        public GetAllExamQueryHandler(IExamService examService)
        {
            _examService = examService;
        }

        public async Task<List<ExamListDTO>> Handle(GetAllExamQuery request, CancellationToken cancellationToken)
        {
            return await _examService.GetAllAsync();
        }
    }

}