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

internal class RemoveItemFromBasketHandler(IBasketRepository repository)
    : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
{
    public async Task<RemoveItemFromBasketResult> Handle(RemoveItemFromBasketCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = await repository.GetBasket(command.UserName, false, cancellationToken);
        shoppingCart.RemoveItem(command.ProductId);
        await repository.SaveChangesAsync(command.UserName, cancellationToken);
        return new RemoveItemFromBasketResult(shoppingCart.Id);
    }
}
