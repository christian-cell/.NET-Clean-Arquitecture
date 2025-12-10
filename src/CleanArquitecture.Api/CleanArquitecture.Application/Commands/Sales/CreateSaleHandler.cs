using NetNinja.Mediator.Abstractions;

namespace CleanArquitecture.Application.Commands.Sales
{
    public class CreateSaleHandler: IRequestHandler<CreateSaleCommand, CreateSaleResponse>
    {
        public async Task<CreateSaleResponse> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
};

