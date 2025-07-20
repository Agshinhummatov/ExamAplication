using Application.DTOs.Exam;
using Application.Helpers.Result;
using MediatR;

namespace Application.Features.Commands.Exam
{
    public record class CreateExamCommand(ExamCreateAndUpdateDto Dto) : IRequest<OperationResult>;
}