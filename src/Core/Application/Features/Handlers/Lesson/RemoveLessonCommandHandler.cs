using Application.Abstractions.Services;
using Application.Features.Commands.Lesson;
using Application.Helpers.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Handlers.Lesson
{
    public class RemoveLessonCommandHandler : IRequestHandler<RemoveLessonCommand, OperationResult>
    {
        private readonly ILessonService _lessonService;

        public RemoveLessonCommandHandler(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task<OperationResult> Handle(RemoveLessonCommand request, CancellationToken cancellationToken)
        {
            return await _lessonService.DeleteAsync(request.Id);
        }
    }
}
