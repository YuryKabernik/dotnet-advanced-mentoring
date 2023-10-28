namespace CartingService.BusinessLogic.Interfaces.Handlers;

public interface IQueryHandler<TRequest, TResult>
{
    Task<TResult> Execute(TRequest request, CancellationToken cancellationToken);
}