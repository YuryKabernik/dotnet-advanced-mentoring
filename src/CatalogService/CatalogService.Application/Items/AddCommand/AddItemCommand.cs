﻿using MediatR;

namespace Application.Items.AddCommand;

public class AddItemCommand : IRequest<AddItemCommandResponse>
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public required Guid Category { get; set; }

    public required decimal Price { get; set; }

    public required int Amount { get; set; }
}
