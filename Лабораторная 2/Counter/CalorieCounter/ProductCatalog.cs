namespace CalorieCounter;

public class ProductCatalog
{
    private List<Product> products = new();

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public Product FindProduct(string name)
    {
        foreach (Product product in products)
        {
            if (product.Name == name)
            {
                return product;
            }
        }
        throw new KeyNotFoundException($"Product \"{name}\" is not in the catalog");
    }
}
