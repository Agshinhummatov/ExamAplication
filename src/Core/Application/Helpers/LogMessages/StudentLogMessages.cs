using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.LogMessages
{
    public static class StudentLogMessages
    {
        public const string CreateAttemptNullDto = "Student creation attempted with null DTO.";
        public const string CreateSuccess = "Student created successfully. Name: {StudentName}";

        public const string DeleteInvalidId = "Student deletion attempted with invalid ID.";
        public const string DeleteNotFound = "Student to delete not found. ID: {StudentId}";
        public const string DeleteSuccess = "Student deleted successfully. ID: {StudentId}";

        public const string UpdateInvalidId = "Student update attempted with invalid ID.";
        public const string UpdateDtoNull = "Student update attempted with null DTO.";
        public const string UpdateNotFound = "Student to update not found. ID: {StudentId}";
        public const string UpdateSuccess = "Student updated successfully. ID: {StudentId}";

        public const string SoftDeleteInvalidId = "Student soft-delete attempted with invalid ID.";
        public const string SoftDeleteNotFound = "Student to soft-delete not found. ID: {StudentId}";
        public const string SoftDeleteSuccess = "Student soft-deleted successfully. ID: {StudentId}";

        public const string FetchAllError = "An error occurred while fetching students.";
    
        public const string UpdateAttemptNullDto = "Student update attempted with null DTO.";


        public const string CreateAttempt = "Student creation attempted. Success: {0}, Message: {1}";


        public const string GetAllSuccess = "Fetched all students successfully. Count: {Count}";
        public const string SearchAttempt = "Student search attempted. SearchText: '{0}', ResultCount: {1}";

        public const string NotFound = "Student not found. ID: {0}";
        public const string GetByIdSuccess = "Fetched student by ID successfully. ID: {0}";

 
        public const string UpdateAttempt = "Student update attempted. ID: {0}, Success: {1}, Message: {2}";
        public const string DeleteAttempt = "Student deletion attempted. ID: {0}, Success: {1}, Message: {2}";
        public const string SoftDeleteAttempt = "Student soft-delete attempted. ID: {0}, Success: {1}, Message: {2}";
     
        public const string GetByIdAttempt = "Fetch student by ID attempted. ID: {0}";

        public const string GetAllFilteredAttempt = "Filtered student list requested. SearchText: {0}, Page: {1}, PageSize: {2}";


    }
}
