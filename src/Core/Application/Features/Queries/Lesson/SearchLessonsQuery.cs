using Application.DTOs.Lesson;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Lesson
{
    public record SearchLessonsQuery(string? SearchText) : IRequest<List<LessonListDto>>;
}
