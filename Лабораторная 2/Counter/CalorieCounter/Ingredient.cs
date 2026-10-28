namespace CalorieCounter;

public class Ingredient
{
    private double massG;
    private Product product;

    public Ingredient(Product product, double massG)
    {
        this.product = product;
        this.massG = massG;
    }

    public double CalculateKcal()
    {
        double kcal = product.CalculateKcal(massG);
        return kcal;
    }
}
