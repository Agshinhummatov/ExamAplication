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
    public class GetFilteredLessonsHandler : IRequestHandler<GetFilteredLessonsQuery, PagedResult<LessonListDto>>
    {
        private readonly ILessonService _lessonService;

        public GetFilteredLessonsHandler(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public async Task<PagedResult<LessonListDto>> Handle(GetFilteredLessonsQuery request, CancellationToken cancellationToken)
        {
            return await _lessonService.GetFilteredLessonsAsync(request.Page, request.PageSize, request.SearchText);
        }
    }
}
