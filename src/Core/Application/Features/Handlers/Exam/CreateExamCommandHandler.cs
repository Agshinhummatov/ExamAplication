using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.DTOs.Exam;
using Application.Features.Commands.Exam;
using Application.Helpers.Result;
using Application.Helpers.SuccesMessages;
using Domain.Entities;
using MediatR;
using System.Diagnostics;

namespace Application.Features.Handlers.Exam
{
    public class CreateExamCommandHandler : IRequestHandler<CreateExamCommand, OperationResult>
    {
        private readonly IExamService _examService;

        public CreateExamCommandHandler(IExamService examService)
        {
            _examService = examService;
        }

        public async Task<OperationResult> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
            return await _examService.CreateExamAsync(request.Dto);
        }
    }

}