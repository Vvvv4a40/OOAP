namespace CalorieCounter.Tests;

public class ProductTests
{
    [Fact]
    public void Constructor_StoresNameAndSource()
    {
        var milk = Sample.Milk();

        Assert.Equal("Молоко", milk.Name);
        Assert.Equal("этикетка", milk.Source);
    }

    [Fact]
    public void GetKcalPer100g_ReturnsLabelValue()
    {
        Assert.Equal(52, Sample.Milk().GetKcalPer100g());
    }
}
