using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Student
{
    public class StudentListDto
    {
        public int Id { get; set; }
        public decimal StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Class { get; set; }
    }
}
