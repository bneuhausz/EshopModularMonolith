namespace Catalog.Products.Features.GetProducts;

public record GetProductsResponse(IEnumerable<ProductDto> Products);

public class GetProductsEndpoint
    : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());
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
