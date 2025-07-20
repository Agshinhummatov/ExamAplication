using Newtonsoft.Json;

namespace ExamAplication.Admin.Response
{
    public class ApiErrorResponse
    {
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
