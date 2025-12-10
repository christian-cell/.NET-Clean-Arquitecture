namespace CleanArquitecture.Application.Commands.Sales
{
    public class CreateSaleResponse: CreateBaseResponse
    {
        public CreateSaleResponse(bool isSuccess, Guid id, string message) : base(isSuccess, id, message)
        {
            IsSuccess = isSuccess;
            Id = id;
            Message = message;
        } 
    }
};

