using MediatR;

namespace CartingService.BusinessLogic.Interfaces.Handlers;

public interface IQueryHandler<TRequest, TResult>
        where TRequest : IRequest<TResult>
{
}