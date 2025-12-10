namespace CleanArquitecture.Application.Commands
{
    public class CreateBaseResponse
    {
        public bool IsSuccess { get; set; }
        public Guid Id { get; set; }
        public string Message { get; set; }

        public CreateBaseResponse(bool isSuccess, Guid id, string message)
        {
            IsSuccess = isSuccess;
            Id = id;
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }
    }
};

