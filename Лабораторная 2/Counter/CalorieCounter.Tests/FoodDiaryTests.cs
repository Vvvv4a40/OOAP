namespace CalorieCounter.Tests;

public class FoodDiaryTests
{
    [Fact]
    public void CalculateDayKcal_SumsPortionsEatenThatDay()
    {
        var diary = new FoodDiary();
        diary.AddPortion(new Portion(Sample.Milk(), 200));
        diary.AddPortion(new Portion(Sample.Porridge(), 110));

        Assert.Equal(104 + 122.4, diary.CalculateDayKcal(DateTime.Today), 6);
    }

    [Fact]
    public void CalculateDayKcal_DayWithoutPortions_ReturnsZero()
    {
        var diary = new FoodDiary();
        diary.AddPortion(new Portion(Sample.Milk(), 200));

        Assert.Equal(0, diary.CalculateDayKcal(DateTime.Today.AddDays(-1)));
    }
}
