using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.ErrorMessages
{
    public static class ExamErrorMessages
    {
        public const string ExamNotFound = "Exam not found.";
        public const string ExamCreateFailed = "An error occurred while creating the exam.";
        public const string ExamUpdateFailed = "An error occurred while updating the exam.";
        public const string UnexpectedError = "An unexpected error occurred.";

        public const string ExamDtoNull = "Exam data cannot be null.";
        public const string InvalidExamId = "Invalid exam ID provided.";



    }
}
