using Application.DTOs.Exam;
using Application.Helpers.Result;
using MediatR;

namespace Application.Features.Queries.Exam
{
    public class GetByIdExamQuery : IRequest<ExamListDTO>
    {
        public GetByIdExamQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}