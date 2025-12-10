using NetNinja.Mediator.Abstractions;

namespace CleanArquitecture.Application.Commands.Sales
{
    public class CreateSaleCommand: IRequest<CreateSaleResponse>
    {
        public string Description { get; set; }
        
        public CreateSaleCommand(string description)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
};

