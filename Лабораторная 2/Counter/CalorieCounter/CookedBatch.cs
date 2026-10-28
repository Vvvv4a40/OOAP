namespace CalorieCounter;

public class CookedBatch : Food
{
    private double readyMassG;
    private double totalKcal;
    private List<Ingredient> ingredients = new();

    public CookedBatch(string name, double readyMassG) : base(name)
    {
        this.readyMassG = readyMassG;
    }

    public void AddIngredient(Product product, double massG)
    {
        Ingredient ingredient = new Ingredient(product, massG);
        ingredients.Add(ingredient);
        double kcal = ingredient.CalculateKcal();
        totalKcal = totalKcal + kcal;
    }

    public override double GetKcalPer100g()
    {
        return totalKcal / readyMassG * 100;
    }

    public override bool CanServe(double massG)
    {
        return base.CanServe(massG) && massG <= readyMassG;
    }
}
