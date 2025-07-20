using ExamAplication.Admin.Models.Exam;
using ExamAplication.Admin.Models.Lesson;

namespace ExamAplication.Admin.Models.ViewModel
{
    public class DashboardChartViewModel
    {
        public List<ExamList> Exams { get; set; }
        public List<LessonList> Lessons { get; set; }
    }
}
