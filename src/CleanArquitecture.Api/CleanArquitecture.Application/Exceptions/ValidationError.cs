namespace CleanArquitecture.Application.Exceptions
{
    public class ValidationError
    {
        public string Error { get; set; }
        public string PropertyName { get; set; }
        public string Code { get; set; } 
        
        /*public ValidationError(string error, string propertyName, string code)
        {
            Error = error ?? throw new ArgumentNullException(nameof(error));
            PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            Code = code ?? throw new ArgumentNullException(nameof(code));
        }*/
    }
};

