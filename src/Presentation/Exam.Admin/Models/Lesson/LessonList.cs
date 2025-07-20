namespace ExamAplication.Admin.Models.Lesson
{
    public class LessonList
    {
        public int Id { get; set; }
        public string LessonCode { get; set; }
        public string LessonName { get; set; }
        public decimal Class { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
    }
}
