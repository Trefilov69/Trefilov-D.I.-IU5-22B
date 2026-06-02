namespace _3Homework3;

public class Animal
{
    public int Id { get; set; }

    public int ZooId { get; set; }

    public Zoo? Zoo { get; set; }

    public string Name { get; set; } = "";

    public double WeightKg { get; set; }
}