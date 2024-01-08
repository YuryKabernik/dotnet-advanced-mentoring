using MediatR;

namespace CartingService.BusinessLogic.Commands.Add;

public record AddCommandRequest(string CartId, NewItem Item) : IRequest;
public record NewItem(string Id, int Quantity);

