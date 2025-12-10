namespace CleanArquitecture.Api.Exceptions
{
    public class ApiException
    {
        public string Message { get; set; }
        public IEnumerable<ValidationError> Errors { get; set; }
        public string Type { get; set; }
        public Dictionary<string, object> Attributes { get; set; }
        public int HttpStatusCode { get; set; }

        public class ValidationError
        {
            public string Property { get; set; }
            public string Message { get; set; }
            public string Code { get; set; }
        } 
    }
};