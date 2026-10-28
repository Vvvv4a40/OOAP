namespace CalorieCounter;

class Program
{
    static void Main(string[] args)
    {
        ProductCatalog catalog = new ProductCatalog();
        catalog.AddProduct(new Product("Хлопья", 352, "этикетка"));
        catalog.AddProduct(new Product("Молоко", 52, "этикетка"));

        FoodDiary diary = new FoodDiary();
        PortionHandler handler = new PortionHandler(catalog, diary);

        Dictionary<string, double> composition = new Dictionary<string, double>
        {
            ["Хлопья"] = 40,
            ["Молоко"] = 200,
        };
        CookedBatch porridge = handler.CreateBatch("Каша", 220, composition);
        Console.WriteLine($"{porridge.Name}: {porridge.GetKcalPer100g():F1} ккал на 100 г");

        Portion? tooBig = handler.Preview(porridge, 300);
        if (tooBig == null)
        {
            Console.WriteLine("300 г больше готовой массы, такую порцию подать нельзя");
        }

        Portion? portion = handler.Preview(porridge, 110);
        if (portion != null)
        {
            Console.WriteLine($"Предпросмотр: {portion.MassG} г, {portion.Kcal:F1} ккал");
        }

        if (handler.Confirm())
        {
            Console.WriteLine("Порция записана в дневник");
        }
        Console.WriteLine($"За сегодня: {diary.CalculateDayKcal(DateTime.Today):F1} ккал");
    }
}
