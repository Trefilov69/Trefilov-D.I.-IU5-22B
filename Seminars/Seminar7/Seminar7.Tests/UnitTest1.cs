using Seminar7;
using Xunit;

namespace Seminar7.Tests;

public class FamilyFineRuleTests
{
    [Fact]
    public void Calculate_WithinGracePeriod_ReturnsZero()
    {
        var rule = new FamilyFineRule();

        decimal fine = rule.Calculate(3);

        Assert.Equal(0m, fine);
    }

    [Fact]
    public void Calculate_AfterGracePeriod_ChargesTwoRublesPerDay()
    {
        var rule = new FamilyFineRule();

        decimal fine = rule.Calculate(8);

        Assert.Equal(6m, fine);
    }

    [Fact]
    public void Calculate_BigOverdue_ReturnsMaxFine()
    {
        var rule = new FamilyFineRule();

        decimal fine = rule.Calculate(100);

        Assert.Equal(100m, fine);
    }

    [Fact]
    public void Calculate_NegativeDays_ThrowsArgumentException()
    {
        var rule = new FamilyFineRule();

        Assert.Throws<ArgumentException>(
            () => rule.Calculate(-1));
    }
}