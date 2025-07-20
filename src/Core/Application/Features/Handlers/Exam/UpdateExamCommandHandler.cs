using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.DTOs.Exam;
using Application.Features.Commands.Exam;
using Application.Helpers.ErrorMessages;
using Application.Helpers.Result;
using Application.Helpers.SuccesMessages;
using MediatR;

namespace Application.Features.Handlers.Exam
{
    public class UpdateExamCommandHandler : IRequestHandler<UpdateExamCommand, OperationResult>
    {
        private readonly IExamService _examService;

        public UpdateExamCommandHandler(IExamService examService)
        {
            _examService = examService;
        }

        public async Task<OperationResult> Handle(UpdateExamCommand request, CancellationToken cancellationToken)
        {
            return await _examService.UpdateExamAsync(request.Id, request.Dto);
        }
    }

}