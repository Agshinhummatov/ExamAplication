using Application.DTOs.Exam;
using Application.Helpers.Result;
using MediatR;

namespace Application.Features.Queries.Exam
{
    public class GetAllExamQuery :IRequest<List<ExamListDTO>>
    {
        public int ExamId { get; set; }
        public string LessonCode { get; set; }
        public decimal StudentNumber { get; set; }
        public DateTime ExamDate { get; set; }
        public decimal Grade { get; set; }
    }
}