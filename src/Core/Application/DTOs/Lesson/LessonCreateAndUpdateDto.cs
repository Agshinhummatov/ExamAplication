using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Lesson
{
    public class LessonCreateAndUpdateDto
    {
        public string LessonCode { get; set; }
        public string LessonName { get; set; }
        public decimal Class { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
    }
}
