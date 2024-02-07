using MediatR;

namespace CartingService.BusinessLogic.Interfaces.Handlers;

public interface ICommandHandler<TRequest> : IRequestHandler<TRequest>
        where TRequest : IRequest
{
}