namespace ExamAplication.Admin.Models.Exam
{
    public class ExamUpdate
    {
        public int Id { get; set; }
        public string LessonCode { get; set; }
        public int StudentNumber { get; set; }
        public DateTime ExamDate { get; set; }
        public int Grade { get; set; }
        public int LessonId { get; set; }
    }
}
