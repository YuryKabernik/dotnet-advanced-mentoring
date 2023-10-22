namespace CartingService.BusinessLogic.Interfaces;

public interface ICommandHandler<TRequest>
{
    Task Execute(TRequest request, CancellationToken cancellationToken);
}
