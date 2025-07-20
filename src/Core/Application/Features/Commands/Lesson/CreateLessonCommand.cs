using Application.DTOs.Lesson;
using Application.Helpers.Result;
using MediatR;


namespace Application.Features.Commands.Lesson
{
    public record class CreateLessonCommand(LessonCreateAndUpdateDto Dto) : IRequest<OperationResult>;
}
