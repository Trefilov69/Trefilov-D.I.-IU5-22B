namespace _3Homework3;

public class Zoo
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public ICollection<Animal> Animals { get; set; }
        = new List<Animal>();
}