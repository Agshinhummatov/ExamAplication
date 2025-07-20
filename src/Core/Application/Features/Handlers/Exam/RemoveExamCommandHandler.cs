using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Features.Commands.Exam;
using Application.Helpers.Result;
using MediatR;

namespace Application.Features.Handlers.Exam
{
    public class RemoveExamCommandHandler : IRequestHandler<RemoveExamCommand, OperationResult>
    {
        private readonly IExamService _examService;

        public RemoveExamCommandHandler(IExamService examService)
        {
            _examService = examService;
        }

        public async Task<OperationResult> Handle(RemoveExamCommand request, CancellationToken cancellationToken)
        {
            return await _examService.DeleteExamAsync(request.Id);
        }
    }
}