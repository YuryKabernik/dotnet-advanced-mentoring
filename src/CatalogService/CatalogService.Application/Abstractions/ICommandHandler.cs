using MediatR;

namespace CatalogService.Application.Abstractions;

internal interface ICommandHandler<TRequest> : IRequestHandler<TRequest>
    where TRequest : IRequest
{
}

internal interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
}
