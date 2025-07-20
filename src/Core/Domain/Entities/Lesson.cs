using Domain.Base;

namespace Domain.Entities
{
    public class Lesson : BaseEntity
    {
        public string LessonCode { get; set; }
        public string LessonName { get; set; }
        public decimal Class { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
    }
}