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
    public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand, OperationResult>
    {
        private readonly ILessonService _lessonService;

        public UpdateLessonCommandHandler(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task<OperationResult> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
        {
            return await _lessonService.UpdateAsync(request.Id, request.Dto);
        }
    }

}
