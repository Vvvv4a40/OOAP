namespace CalorieCounter;

public class Portion
{
    public double MassG { get; }
    public double Kcal { get; }
    private Food food;
    public DateTime EatenAt { get; }

    public Portion(Food food, double massG)
    {
        this.food = food;
        MassG = massG;
        EatenAt = DateTime.Now;
        Kcal = food.CalculateKcal(massG);
    }
}
