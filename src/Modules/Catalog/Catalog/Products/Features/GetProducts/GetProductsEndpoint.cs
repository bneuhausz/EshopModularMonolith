﻿namespace Catalog.Products.Features.GetProducts;

public record GetProductsResponse(PaginatedResult<ProductDto> Products);

public class GetProductsEndpoint
    : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery(request));
            var respone = result.Adapt<GetProductsResponse>();
            return Results.Ok(respone);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}
