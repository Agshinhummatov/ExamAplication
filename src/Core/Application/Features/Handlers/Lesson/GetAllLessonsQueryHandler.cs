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
    public class GetAllLessonsQueryHandler : IRequestHandler<GetAllLessonsQuery, List<LessonListDto>>
    {
        private readonly ILessonService _lessonService;

        public GetAllLessonsQueryHandler(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task<List<LessonListDto>> Handle(GetAllLessonsQuery request, CancellationToken cancellationToken)
        {
            return await _lessonService.GetAllAsync();
        }
    }

}
