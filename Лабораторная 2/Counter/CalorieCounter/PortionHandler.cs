namespace CalorieCounter;

public class PortionHandler
{
    private FoodDiary diary;
    private Portion? preview;
    private ProductCatalog catalog;

    public PortionHandler(ProductCatalog catalog, FoodDiary diary)
    {
        this.catalog = catalog;
        this.diary = diary;
    }

    public CookedBatch CreateBatch(string name, double readyMassG, Dictionary<string, double> composition)
    {
        CookedBatch batch = new CookedBatch(name, readyMassG);
        foreach (var (productName, massG) in composition)
        {
            Product product = catalog.FindProduct(productName);
            batch.AddIngredient(product, massG);
        }
        return batch;
    }

    public Portion? Preview(Food food, double massG)
    {
        bool canServe = food.CanServe(massG);
        if (canServe)
        {
            preview = new Portion(food, massG);
        }
        else
        {
            // An earlier preview must not stay confirmable after a failed one.
            preview = null;
        }
        return preview;
    }

    public bool Confirm()
    {
        if (preview == null)
        {
            return false;
        }
        diary.AddPortion(preview);
        // The preview is used up, so a second Confirm does not add the same portion again.
        preview = null;
        return true;
    }
}
