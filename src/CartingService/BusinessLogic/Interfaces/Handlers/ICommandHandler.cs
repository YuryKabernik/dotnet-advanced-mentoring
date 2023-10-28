namespace CartingService.BusinessLogic.Interfaces.Handlers;

public interface ICommandHandler<T>
{
    Task Execute(T request, CancellationToken cancellationToken);
}