using System.ComponentModel.DataAnnotations;

namespace ExamAplication.Admin.Models.Exam
{
    public class ExamCreate
    {
        [Required(ErrorMessage = "Lesson code is required.")]
        [StringLength(3, ErrorMessage = "Lesson code cannot be longer than 30 characters.")]
        public string LessonCode { get; set; }
        public int StudentNumber { get; set; }
        public DateTime ExamDate { get; set; }

        [Range(0, 9, ErrorMessage = "Grade must be a single digit between 0 and 9.")]
        public int Grade { get; set; }
        public int LessonId { get; set; }
    }
}



