namespace Seminar7;

public class FamilyFineRule
{
    private const int GraceDays = 5;
    private const decimal DailyFine = 2m;
    private const decimal MaxFine = 100m;

    public decimal Calculate(int daysOverdue)
    {
        if (daysOverdue < 0)
            throw new ArgumentException("Количество дней не может быть отрицательным");

        if (daysOverdue <= GraceDays)
            return 0m;

        decimal fine = (daysOverdue - GraceDays) * DailyFine;

        return fine > MaxFine ? MaxFine : fine;
    }
}