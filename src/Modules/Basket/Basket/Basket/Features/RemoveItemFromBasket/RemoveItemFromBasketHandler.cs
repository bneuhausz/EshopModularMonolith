
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Basket.Basket.Features.RemoveItemFromBasket;

public record RemoveItemFromBasketCommand(string UserName, Guid ProductId)
    : ICommand<RemoveItemFromBasketResult>;

public record RemoveItemFromBasketResult(Guid Id);

public class RemoveItemFromBasketCommandValidator : AbstractValidator<RemoveItemFromBasketCommand>
{
    public RemoveItemFromBasketCommandValidator()
    {
        RuleFor(c => c.UserName).NotEmpty().WithMessage("UserName is requred");
        RuleFor(c => c.ProductId).NotEmpty().WithMessage("ProductId is requred");
    }
}

internal class RemoveItemFromBasketHandler(BasketDbContext dbContext)
    : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
{
    public async Task<RemoveItemFromBasketResult> Handle(RemoveItemFromBasketCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = await dbContext.ShoppingCarts
            .Include(sc => sc.Items)
            .SingleOrDefaultAsync(sc => sc.UserName == command.UserName, cancellationToken);

        if (shoppingCart is null)
        {
            throw new BasketNotFoundException(command.UserName);
        }

        shoppingCart.RemoveItem(command.ProductId);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new RemoveItemFromBasketResult(shoppingCart.Id);
    }
}
