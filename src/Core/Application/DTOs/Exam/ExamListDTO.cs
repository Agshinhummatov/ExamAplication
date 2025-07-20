using Application.DTOs.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Exam
{
    public class ExamListDTO
    {
        public int Id { get; set; }
        public string LessonCode { get; set; }
        public decimal StudentNumber { get; set; }
        public DateTime ExamDate { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Grade { get; set; }
        public LessonListDto Lesson { get; set; }
    }
}
