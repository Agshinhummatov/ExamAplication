using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.LogMessages
{
    public static class ExamLogMessages
    {
        public const string CreateAttempt = "Attempting to create exam. Success: {Success}, Message: {Message}";
        public const string UpdateAttempt = "Attempting to update exam with Id {Id}. Success: {Success}, Message: {Message}";
        public const string DeleteAttempt = "Attempting to delete exam with Id {Id}. Success: {Success}, Message: {Message}";

        public const string GetAllSuccess = "Successfully fetched all exams.";
        public const string GetAllError = "Error occurred while fetching all exams.";

        public const string GetByIdAttempt = "Fetching exam with Id {Id}.";
        public const string GetByIdError = "Error occurred while fetching exam with Id.";

        public const string InternalServerError = "Internal server error.";

        public const string CreateAttemptNullDto = "Create operation received null Exam DTO.";
        public const string CreateSuccess = "Exam successfully created. StudentNumber: {0}";
        public const string DeleteInvalidId = "Attempt to delete exam with invalid ID.";
        public const string DeleteNotFound = "Exam not found with ID: {0}";
        public const string DeleteSuccess = "Exam successfully deleted. ID: {0}";
        public const string UpdateInvalidId = "Invalid ID provided for exam update.";
        public const string UpdateDtoNull = "Update operation received null DTO.";
        public const string UpdateNotFound = "Exam to update not found with ID: {0}";
        public const string UpdateSuccess = "Exam successfully updated. ID: {0}";
        public const string GetFilteredExam = "Filtered exams fetched. Page: {Page}, PageSize: {PageSize}, SearchText: {SearchText} MinGrade : {minGrade}";
    }
}
