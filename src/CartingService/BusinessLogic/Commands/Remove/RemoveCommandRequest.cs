using MediatR;

namespace CartingService.BusinessLogic.Commands.Remove;

public record RemoveCommandRequest(string CartId, string ItemId) : IRequest;
