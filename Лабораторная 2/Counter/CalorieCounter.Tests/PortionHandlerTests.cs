namespace CalorieCounter.Tests;

public class PortionHandlerTests
{
    private readonly FoodDiary diary = new();
    private readonly PortionHandler handler;

    public PortionHandlerTests()
    {
        var catalog = new ProductCatalog();
        catalog.AddProduct(Sample.Flakes());
        catalog.AddProduct(Sample.Milk());
        handler = new PortionHandler(catalog, diary);
    }

    private CookedBatch CreatePorridge() =>
        handler.CreateBatch("Каша", Sample.PorridgeReadyMassG,
            new Dictionary<string, double> { ["Хлопья"] = 40, ["Молоко"] = 200 });

    [Fact]
    public void CreateBatch_TakesProductsFromCatalog()
    {
        var batch = CreatePorridge();

        Assert.Equal("Каша", batch.Name);
        Assert.Equal(Sample.PorridgeKcal / Sample.PorridgeReadyMassG * 100, batch.GetKcalPer100g(), 6);
    }

    [Fact]
    public void Preview_ServableMass_ReturnsPortionWithKcal()
    {
        var portion = handler.Preview(CreatePorridge(), 110);

        Assert.NotNull(portion);
        Assert.Equal(122.4, portion.Kcal, 6);
    }

    [Fact]
    public void Preview_MassAboveReadyMass_ReturnsNull()
    {
        Assert.Null(handler.Preview(CreatePorridge(), 300));
    }

    [Fact]
    public void Confirm_AfterPreview_AddsPortionToDiary()
    {
        handler.Preview(CreatePorridge(), 110);

        Assert.True(handler.Confirm());
        Assert.Equal(122.4, diary.CalculateDayKcal(DateTime.Today), 6);
    }

    [Fact]
    public void Confirm_WithoutPreview_ReturnsFalse()
    {
        Assert.False(handler.Confirm());
    }

    [Fact]
    public void Confirm_AfterFailedPreview_DoesNotAddEarlierPreview()
    {
        var batch = CreatePorridge();
        handler.Preview(batch, 110);
        handler.Preview(batch, 300);

        Assert.False(handler.Confirm());
        Assert.Equal(0, diary.CalculateDayKcal(DateTime.Today));
    }

    [Fact]
    public void Confirm_Twice_AddsPortionOnce()
    {
        handler.Preview(CreatePorridge(), 110);
        handler.Confirm();

        Assert.False(handler.Confirm());
        Assert.Equal(122.4, diary.CalculateDayKcal(DateTime.Today), 6);
    }
}
