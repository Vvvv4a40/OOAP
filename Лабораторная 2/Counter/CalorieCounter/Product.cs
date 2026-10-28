namespace CalorieCounter;

public class Product : Food
{
    private double kcalPer100g;

    public string Source { get; }

    public Product(string name, double kcalPer100g, string source) : base(name)
    {
        this.kcalPer100g = kcalPer100g;
        Source = source;
    }

    public override double GetKcalPer100g()
    {
        return kcalPer100g;
    }
}
