using Application.Abstractions.Services;
using Application.Features.Commands.Lesson;
using Application.Helpers.Result;
using MediatR;

namespace Application.Features.Handlers.Lesson
{
    public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, OperationResult>
    {
        private readonly ILessonService _lessonService;

        public CreateLessonCommandHandler(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task<OperationResult> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            return await _lessonService.CreateAsync(request.Dto);
        }
    }
}
