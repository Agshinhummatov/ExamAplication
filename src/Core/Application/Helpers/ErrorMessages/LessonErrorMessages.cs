using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.ErrorMessages
{
    public static class LessonErrorMessages
    {
        public const string LessonDtoNull = "The Lesson DTO is null.";
        public const string InvalidLessonId = "Invalid Lesson ID.";
        public const string LessonNotFound = "Lesson not found.";
        public const string LessonDeleteFailed = "Lesson deletion failed.";
        public const string LessonUpdateFailed = "Lesson update failed.";
        public const string LessonSoftDeleteFailed = "Lesson soft deletion failed.";
    }
}
