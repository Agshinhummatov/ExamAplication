using System.Net;

namespace ExamAplication.Admin.Models.ViewModel
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? Body { get; set; }
    }
}
