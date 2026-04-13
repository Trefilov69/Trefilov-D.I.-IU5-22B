class Animal
{
    public int Id { get; set; }
    public int ZooId { get; set; }
    public string Name { get; set; }

    private double _weight;
    public double WeightKg
    {
        get => _weight;
        set
        {
            if (value < 0)
                throw new ArgumentException("Вес < 0");
            _weight = value;
        }
    }

    public Animal(int id, int zooId, string name, double weight)
    {
        Id = id;
        ZooId = zooId;
        Name = name;
        WeightKg = weight;
    }

    public Animal() : this(0, 0, "", 0) { }

    public override string ToString()
        => $"[{Id}] {Name}, zoo={ZooId}, kg={WeightKg}";
}
