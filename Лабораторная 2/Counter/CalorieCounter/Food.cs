namespace CalorieCounter;

public abstract class Food
{
    public string Name { get; }

    protected Food(string name)
    {
        Name = name;
    }

    public abstract double GetKcalPer100g();

    public double CalculateKcal(double massG)
    {
        double kcalPer100g = GetKcalPer100g();
        return kcalPer100g * massG / 100;
    }

    public virtual bool CanServe(double massG)
    {
        return massG > 0;
    }
}
