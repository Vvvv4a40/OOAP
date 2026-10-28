namespace CalorieCounter.Tests;

// Food is abstract, so its own logic is checked through Product.
public class FoodTests
{
    [Fact]
    public void CalculateKcal_ScalesKcalPer100gToMass()
    {
        Assert.Equal(104, Sample.Milk().CalculateKcal(200), 6);
        Assert.Equal(140.8, Sample.Flakes().CalculateKcal(40), 6);
    }

    [Fact]
    public void CanServe_PositiveMass_ReturnsTrue()
    {
        Assert.True(Sample.Milk().CanServe(250));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void CanServe_ZeroOrNegativeMass_ReturnsFalse(double massG)
    {
        Assert.False(Sample.Milk().CanServe(massG));
    }
}
