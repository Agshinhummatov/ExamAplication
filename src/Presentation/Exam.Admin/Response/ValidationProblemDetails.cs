using Newtonsoft.Json;

namespace ExamAplication.Admin.Response
{
    public class ValidationProblemDetails
    {
        [JsonProperty("errors")]
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
