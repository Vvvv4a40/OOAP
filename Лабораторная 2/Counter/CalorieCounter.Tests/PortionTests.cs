namespace CalorieCounter.Tests;

public class PortionTests
{
    [Fact]
    public void Constructor_StoresMassAndCreationTime()
    {
        DateTime before = DateTime.Now;
        var portion = new Portion(Sample.Milk(), 200);

        Assert.Equal(200, portion.MassG);
        Assert.InRange(portion.EatenAt, before, DateTime.Now);
    }

    [Fact]
    public void Constructor_CalculatesKcalFromFood()
    {
        var portion = new Portion(Sample.Porridge(), 110);

        Assert.Equal(122.4, portion.Kcal, 6);
    }
}
