using MediatR;

namespace CartingService.BusinessLogic.Queries.Items;

public record ItemsQueryRequest(string CartId) : IRequest<ItemsQueryResponse>;
