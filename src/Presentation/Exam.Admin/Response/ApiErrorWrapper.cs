namespace ExamAplication.Admin.Response
{
    public class ApiErrorWrapper
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
