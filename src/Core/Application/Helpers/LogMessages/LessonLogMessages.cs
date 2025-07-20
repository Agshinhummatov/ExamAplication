using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.LogMessages
{
    public static class LessonLogMessages
    {
        public const string CreateAttemptNullDto = "Lesson creation failed: DTO is null.";
        public const string CreateSuccess = "Lesson created successfully. Code: {LessonCode}";

        public const string DeleteInvalidId = "Lesson delete failed: Invalid ID.";
        public const string DeleteNotFound = "Lesson delete failed: Lesson not found. ID: {LessonId}";
        public const string DeleteSuccess = "Lesson deleted successfully. ID: {LessonId}";

        public const string UpdateInvalidId = "Lesson update failed: Invalid ID.";
        public const string UpdateDtoNull = "Lesson update failed: DTO is null.";
        public const string UpdateNotFound = "Lesson update failed: Lesson not found. ID: {LessonId}";
        public const string UpdateSuccess = "Lesson updated successfully. ID: {LessonId}";

        public const string SoftDeleteInvalidId = "Lesson soft-delete failed: Invalid ID.";
        public const string SoftDeleteNotFound = "Lesson soft-delete failed: Lesson not found. ID: {LessonId}";
        public const string SoftDeleteSuccess = "Lesson soft-deleted successfully. ID: {LessonId}";

        public const string CreateAttempt = "Lesson creation attempted. Success: {0}, Message: {1}";
        public const string UpdateAttempt = "Lesson update attempted. ID: {0}, Success: {1}, Message: {2}";
        public const string DeleteAttempt = "Lesson deletion attempted. ID: {0}, Success: {1}, Message: {2}";
        public const string SoftDeleteAttempt = "Lesson soft-delete attempted. ID: {0}, Success: {1}, Message: {2}";
        public const string GetAllSuccess = "Fetched all lessons successfully.";
        public const string SearchAttempt = "Lesson search attempted. SearchText: '{0}', ResultCount: {1}";
        public const string GetByIdAttempt = "Get lesson by ID attempted. ID: {0}";
        public const string GetFilteredLessons = "Filtered lessons fetched. Page: {Page}, PageSize: {PageSize}, SearchText: {SearchText}";
       
    }
}
