
using System;

namespace Play.Catalog.Service.Dto
{
    public record ItemDto(Guid Id, string Name, string Description, decimal price, DateTimeOffset CreatedDate);

    public record CreateItemDto(string Name, string Description, decimal price);
}