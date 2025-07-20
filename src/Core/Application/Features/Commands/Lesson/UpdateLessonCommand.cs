using Application.DTOs.Lesson;
using Application.Helpers.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Lesson
{
    public record UpdateLessonCommand(int Id, LessonCreateAndUpdateDto Dto) : IRequest<OperationResult>;

}
