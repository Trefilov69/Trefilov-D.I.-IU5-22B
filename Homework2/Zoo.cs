class Zoo
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Zoo(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public Zoo() : this(0, "") { }

    public override string ToString() => $"[{Id}] {Name}";
}