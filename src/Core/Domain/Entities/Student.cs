using Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Student : BaseEntity
    {
        [Range(0, 99999, ErrorMessage = "StudentNumber must be a non-negative 5-digit number.")]
        public int StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Class { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
    }
}