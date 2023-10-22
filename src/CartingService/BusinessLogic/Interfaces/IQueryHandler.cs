namespace CartingService.BusinessLogic.Interfaces;

public interface IQueryHandler<TRequest, TResponse>
{
    Task<TResponse> Execute(TRequest request, CancellationToken cancellationToken);
}
