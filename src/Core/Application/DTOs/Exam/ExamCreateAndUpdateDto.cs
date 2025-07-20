using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Exam
{
    public class ExamCreateAndUpdateDto
    {
        [Required]
        public string LessonCode { get; set; }

        [Required]
        public int StudentNumber { get; set; }

        [Required]
        public DateTime ExamDate { get; set; }

        [Required]
        public int Grade { get; set; }

        [Required]
        public int LessonId { get; set; }
    }

}
