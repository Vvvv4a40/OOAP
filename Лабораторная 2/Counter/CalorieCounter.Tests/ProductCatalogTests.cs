namespace CalorieCounter.Tests;

public class ProductCatalogTests
{
    [Fact]
    public void FindProduct_AddedName_ReturnsThatProduct()
    {
        var catalog = new ProductCatalog();
        var milk = Sample.Milk();
        catalog.AddProduct(Sample.Flakes());
        catalog.AddProduct(milk);

        Assert.Same(milk, catalog.FindProduct("Молоко"));
    }

    [Fact]
    public void FindProduct_UnknownName_ThrowsKeyNotFoundException()
    {
        var catalog = new ProductCatalog();
        catalog.AddProduct(Sample.Milk());

        Assert.Throws<KeyNotFoundException>(() => catalog.FindProduct("Сахар"));
    }
}
