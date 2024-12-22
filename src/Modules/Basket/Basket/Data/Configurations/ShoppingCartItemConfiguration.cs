namespace Basket.Data.Configurations;
internal class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
    {
        builder.HasKey(sci => sci.Id);
        builder.Property(sci => sci.ProductId).IsRequired();
        builder.Property(sci => sci.Quantity).IsRequired();
        builder.Property(sci => sci.Color);
        builder.Property(sci => sci.Price).IsRequired();
        builder.Property(sci => sci.ProductName).IsRequired();
    }
}
