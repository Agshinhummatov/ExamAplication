using Application.DTOs.Lesson;
using MediatR;
using Application.Helpers.Result;

namespace Application.Features.Queries.Lesson
{
    public record GetAllLessonsQuery : IRequest<List<LessonListDto>>;

}
