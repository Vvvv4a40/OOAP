namespace CalorieCounter.Tests;

public class CookedBatchTests
{
    [Fact]
    public void GetKcalPer100g_SpreadsIngredientKcalOverReadyMass()
    {
        double expected = Sample.PorridgeKcal / Sample.PorridgeReadyMassG * 100;

        Assert.Equal(expected, Sample.Porridge().GetKcalPer100g(), 6);
    }

    [Fact]
    public void CalculateKcal_HalfOfReadyMass_GivesHalfOfBatchKcal()
    {
        Assert.Equal(122.4, Sample.Porridge().CalculateKcal(110), 6);
    }

    [Theory]
    [InlineData(110)]
    [InlineData(220)]
    public void CanServe_MassUpToReadyMass_ReturnsTrue(double massG)
    {
        Assert.True(Sample.Porridge().CanServe(massG));
    }

    [Theory]
    [InlineData(221)]
    [InlineData(0)]
    public void CanServe_MassAboveReadyMassOrNotPositive_ReturnsFalse(double massG)
    {
        Assert.False(Sample.Porridge().CanServe(massG));
    }
}
