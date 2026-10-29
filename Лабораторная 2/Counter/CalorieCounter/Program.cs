using System.Globalization;

namespace CalorieCounter;

class Program
{
    static void Main(string[] args)
    {
        ProductCatalog catalog = new ProductCatalog();
        FoodDiary diary = new FoodDiary();
        PortionHandler handler = new PortionHandler(catalog, diary);
        List<Product> products = new List<Product>();
        List<Food> foods = new List<Food>();
        List<string> eaten = new List<string>();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1 — добавить продукт, 2 — приготовить блюдо, 3 — съесть, 4 — сколько съедено, 0 — выход");
            switch (Ask("Выбор: ").Trim())
            {
                case "1":
                    AddProduct(catalog, products, foods);
                    break;
                case "2":
                    CookDish(handler, products, foods);
                    break;
                case "3":
                    Eat(handler, foods, eaten);
                    break;
                case "4":
                    ShowEaten(diary, eaten);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Нет такого пункта");
                    break;
            }
        }
    }

    static void AddProduct(ProductCatalog catalog, List<Product> products, List<Food> foods)
    {
        string name = Ask("Название: ").Trim();
        if (name == "")
        {
            Console.WriteLine("Название не может быть пустым");
            return;
        }
        if (products.Exists(product => product.Name == name))
        {
            Console.WriteLine($"Продукт «{name}» уже есть");
            return;
        }
        double kcalPer100g = AskPositive("Ккал на 100 г (с этикетки): ");

        Product added = new Product(name, kcalPer100g, "этикетка");
        catalog.AddProduct(added);
        products.Add(added);
        foods.Add(added);
        Console.WriteLine($"Добавлен продукт «{name}», {kcalPer100g} ккал на 100 г");
    }

    static void CookDish(PortionHandler handler, List<Product> products, List<Food> foods)
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Сначала добавь продукты: пункт 1");
            return;
        }
        string name = Ask("Название блюда: ").Trim();
        if (name == "")
        {
            Console.WriteLine("Название не может быть пустым");
            return;
        }

        Dictionary<string, double> composition = new Dictionary<string, double>();
        while (true)
        {
            ShowFoods(products);
            string choice = Ask("Номер продукта (пустая строка — состав готов): ");
            if (choice.Trim() == "")
            {
                break;
            }
            Product? product = Pick(products, choice);
            if (product == null)
            {
                Console.WriteLine("Нет такого номера");
                continue;
            }
            double massG = AskPositive($"Сколько граммов «{product.Name}» положил: ");
            composition[product.Name] = composition.GetValueOrDefault(product.Name) + massG;
        }
        if (composition.Count == 0)
        {
            Console.WriteLine("Блюдо без продуктов не создано");
            return;
        }

        double readyMassG = AskPositive("Вес готового блюда, г: ");

        CookedBatch dish = handler.CreateBatch(name, readyMassG, composition);
        foods.Add(dish);
        Console.WriteLine($"Блюдо «{name}»: всего {dish.CalculateKcal(readyMassG):F1} ккал, "
            + $"{dish.GetKcalPer100g():F1} ккал на 100 г");
    }

    static void Eat(PortionHandler handler, List<Food> foods, List<string> eaten)
    {
        if (foods.Count == 0)
        {
            Console.WriteLine("Сначала добавь продукт или приготовь блюдо");
            return;
        }
        ShowFoods(foods);
        Food? food = Pick(foods, Ask("Номер: "));
        if (food == null)
        {
            Console.WriteLine("Нет такого номера");
            return;
        }
        double massG = AskPositive("Сколько граммов: ");

        Portion? portion = handler.Preview(food, massG);
        if (portion == null)
        {
            Console.WriteLine("Такую порцию подать нельзя: она больше веса готового блюда");
            return;
        }
        string answer = Ask($"Будет {portion.Kcal:F1} ккал. Записать? (д/н): ").Trim().ToLowerInvariant();
        if (answer != "д" && answer != "да" && answer != "y" && answer != "yes")
        {
            Console.WriteLine("Не записано");
            return;
        }
        handler.Confirm();
        string line = $"{food.Name}, {portion.MassG} г — {portion.Kcal:F1} ккал";
        eaten.Add(line);
        Console.WriteLine($"Записано: {line}");
    }

    static void ShowEaten(FoodDiary diary, List<string> eaten)
    {
        if (eaten.Count == 0)
        {
            Console.WriteLine("Пока ничего не съедено");
            return;
        }
        foreach (string line in eaten)
        {
            Console.WriteLine($"  {line}");
        }
        Console.WriteLine($"Всего за сегодня: {diary.CalculateDayKcal(DateTime.Today):F1} ккал");
    }

    static void ShowFoods<T>(List<T> foods) where T : Food
    {
        for (int i = 0; i < foods.Count; i++)
        {
            string kind = foods[i] is CookedBatch ? "блюдо" : "продукт";
            Console.WriteLine($"  {i + 1}. {foods[i].Name} ({kind}) — {foods[i].GetKcalPer100g():F1} ккал на 100 г");
        }
    }

    static T? Pick<T>(List<T> items, string choice) where T : class
    {
        if (int.TryParse(choice.Trim(), out int number) && number >= 1 && number <= items.Count)
        {
            return items[number - 1];
        }
        return null;
    }

    static string Ask(string prompt)
    {
        Console.Write(prompt);
        string? line = Console.ReadLine();
        if (line == null)
        {
            // Input has ended, for example when it was redirected from a file.
            Environment.Exit(0);
        }
        return line;
    }

    static double AskPositive(string prompt)
    {
        while (true)
        {
            string text = Ask(prompt).Trim().Replace(',', '.');
            if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double value) && value > 0)
            {
                return value;
            }
            Console.WriteLine("Нужно число больше нуля, например 110 или 52,5");
        }
    }
}
