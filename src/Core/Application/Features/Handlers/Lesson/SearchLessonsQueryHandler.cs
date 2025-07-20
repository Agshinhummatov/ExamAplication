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
    public class SearchLessonsQueryHandler : IRequestHandler<SearchLessonsQuery, List<LessonListDto>>
    {
        private readonly ILessonService _lessonService;

        public SearchLessonsQueryHandler(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task<List<LessonListDto>> Handle(SearchLessonsQuery request, CancellationToken cancellationToken)
        {
            return await _lessonService.SearchAsync(request.SearchText);
        }
    }

}
