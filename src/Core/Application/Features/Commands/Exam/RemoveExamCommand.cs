using Application.Helpers.Result;
using MediatR;

namespace Application.Features.Commands.Exam
{
    public record RemoveExamCommand : IRequest<OperationResult>
    {
        public RemoveExamCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}