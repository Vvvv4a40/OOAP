namespace CalorieCounter.Tests;

// The worked example from the report: 40 g of flakes and 200 g of milk cooked into 220 g of porridge.
internal static class Sample
{
    public const double PorridgeKcal = 40 * 352 / 100.0 + 200 * 52 / 100.0;
    public const double PorridgeReadyMassG = 220;

    public static Product Flakes() => new("Хлопья", 352, "этикетка");

    public static Product Milk() => new("Молоко", 52, "этикетка");

    public static CookedBatch Porridge()
    {
        var batch = new CookedBatch("Каша", PorridgeReadyMassG);
        batch.AddIngredient(Flakes(), 40);
        batch.AddIngredient(Milk(), 200);
        return batch;
    }
}
