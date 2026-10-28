namespace CalorieCounter;

public class FoodDiary
{
    private List<Portion> portions = new();

    public void AddPortion(Portion portion)
    {
        portions.Add(portion);
    }

    public double CalculateDayKcal(DateTime day)
    {
        double dayKcal = 0;
        foreach (Portion portion in portions)
        {
            if (portion.EatenAt.Date == day.Date)
            {
                dayKcal = dayKcal + portion.Kcal;
            }
        }
        return dayKcal;
    }
}
