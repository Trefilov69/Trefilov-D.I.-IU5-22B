namespace _3Homework3
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
                SeedData(context);
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }

        private static void SeedData(AppDbContext context)
        {
            if (context.Zoos.Any() || context.Animals.Any())
                return;

            var zoos = new List<Zoo>
            {
                new Zoo { Name = "Московский зоопарк" },
                new Zoo { Name = "Ленинградский зоопарк" },
                new Zoo { Name = "Новосибирский зоопарк" },
                new Zoo { Name = "Казанский зооботсад" }
            };

            context.Zoos.AddRange(zoos);
            context.SaveChanges();

            var animals = new List<Animal>
            {
                new Animal { Name = "Лев", ZooId = zoos[0].Id, WeightKg = 190 },
                new Animal { Name = "Тигр", ZooId = zoos[0].Id, WeightKg = 220 },
                new Animal { Name = "Слон", ZooId = zoos[0].Id, WeightKg = 5400 },

                new Animal { Name = "Пингвин", ZooId = zoos[1].Id, WeightKg = 25 },
                new Animal { Name = "Медведь", ZooId = zoos[1].Id, WeightKg = 350 },
                new Animal { Name = "Волк", ZooId = zoos[1].Id, WeightKg = 60 },

                new Animal { Name = "Жираф", ZooId = zoos[2].Id, WeightKg = 900 },
                new Animal { Name = "Зебра", ZooId = zoos[2].Id, WeightKg = 300 },
                new Animal { Name = "Рысь", ZooId = zoos[2].Id, WeightKg = 30 },

                new Animal { Name = "Обезьяна", ZooId = zoos[3].Id, WeightKg = 45 },
                new Animal { Name = "Крокодил", ZooId = zoos[3].Id, WeightKg = 500 },
                new Animal { Name = "Верблюд", ZooId = zoos[3].Id, WeightKg = 600 }
            };

            context.Animals.AddRange(animals);
            context.SaveChanges();
        }
    }
}