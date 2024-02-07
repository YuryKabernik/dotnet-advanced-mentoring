using MediatR;

namespace CartingService.BusinessLogic.Interfaces.Handlers;

public interface IQueryHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
        where TRequest : IRequest<TResult>
{
}