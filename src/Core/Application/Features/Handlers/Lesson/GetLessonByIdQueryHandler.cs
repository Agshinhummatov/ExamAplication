using Application.Abstractions.Services;
using Application.DTOs.Lesson;
using Application.Features.Queries.Lesson;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Handlers.Lesson
{
    public class GetLessonByIdQueryHandler : IRequestHandler<GetLessonByIdQuery, LessonListDto?>
    {
        private readonly ILessonService _lessonService;

        public GetLessonByIdQueryHandler(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task<LessonListDto?> Handle(GetLessonByIdQuery request, CancellationToken cancellationToken)
        {
            return await _lessonService.GetByIdAsync(request.Id);
        }
    }

}
