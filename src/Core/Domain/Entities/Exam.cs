using Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Exam : BaseEntity
    {
        public string LessonCode { get; set; }

        [Range(0, 99999, ErrorMessage = "StudentNumber must be a non-negative 5-digit number.")]
        public int StudentNumber { get; set; }
        public DateTime ExamDate { get; set; }
        public int Grade { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}