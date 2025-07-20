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
    public class SoftDeleteLessonCommandHandler : IRequestHandler<SoftDeleteLessonCommand, OperationResult>
    {
        private readonly ILessonService _lessonService;

        public SoftDeleteLessonCommandHandler(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task<OperationResult> Handle(SoftDeleteLessonCommand request, CancellationToken cancellationToken)
        {
            return await _lessonService.SoftDeleteAsync(request.Id);
        }
    }
}
