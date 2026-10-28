namespace CalorieCounter.Tests;

public class IngredientTests
{
    [Fact]
    public void CalculateKcal_ReturnsKcalOfItsMass()
    {
        var ingredient = new Ingredient(Sample.Milk(), 200);

        Assert.Equal(104, ingredient.CalculateKcal(), 6);
    }
}
