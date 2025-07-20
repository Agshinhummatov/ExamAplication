using Application.DTOs.Exam;
using Application.Helpers.Result;
using MediatR;

namespace Application.Features.Commands.Exam
{
    public record UpdateExamCommand(int Id, ExamCreateAndUpdateDto Dto) : IRequest<OperationResult>;

}