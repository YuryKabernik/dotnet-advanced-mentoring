using MediatR;

namespace Application.Items.UpdateCommand;

public record UpdateItemCommand(Guid Id, ItemUpdateValues Values) : IRequest<UpdateItemCommandResponse>;

public class ItemUpdateValues
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public Guid? Category { get; set; }

    public decimal? Price { get; set; }

    public int? Amount { get; set; }
}
