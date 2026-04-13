using Microsoft.Data.Sqlite;

class DatabaseManager
{
    private string _cs;

    public DatabaseManager(string path)
    {
        _cs = $"Data Source={path}";
    }

    public void InitializeDatabase(string zooCsv, string animalCsv)
    {
        using var c = new SqliteConnection(_cs);
        c.Open();

        var cmd = c.CreateCommand();
        cmd.CommandText = @"
        CREATE TABLE IF NOT EXISTS zoo(
            zoo_id INTEGER PRIMARY KEY,
            zoo_name TEXT NOT NULL
        );

        CREATE TABLE IF NOT EXISTS animal(
            animal_id INTEGER PRIMARY KEY AUTOINCREMENT,
            zoo_id INTEGER NOT NULL,
            animal_name TEXT NOT NULL,
            weight REAL NOT NULL,
            FOREIGN KEY (zoo_id) REFERENCES zoo(zoo_id)
        );";
        cmd.ExecuteNonQuery();

        var checkZoo = c.CreateCommand();
        checkZoo.CommandText = "SELECT COUNT(*) FROM zoo";
        long zooCount = (long)checkZoo.ExecuteScalar();

        if (zooCount == 0 && File.Exists(zooCsv))
        {
            string[] lines = File.ReadAllLines(zooCsv);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(';');
                if (parts.Length < 2) continue;

                var insert = c.CreateCommand();
                insert.CommandText = "INSERT INTO zoo (zoo_id, zoo_name) VALUES (@id, @name)";
                insert.Parameters.AddWithValue("@id", int.Parse(parts[0]));
                insert.Parameters.AddWithValue("@name", parts[1]);
                insert.ExecuteNonQuery();
            }
        }

        var checkAnimal = c.CreateCommand();
        checkAnimal.CommandText = "SELECT COUNT(*) FROM animal";
        long animalCount = (long)checkAnimal.ExecuteScalar();

        if (animalCount == 0 && File.Exists(animalCsv))
        {
            string[] lines = File.ReadAllLines(animalCsv);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(';');
                if (parts.Length < 4) continue;

                var insert = c.CreateCommand();
                insert.CommandText = @"
                    INSERT INTO animal (animal_id, zoo_id, animal_name, weight)
                    VALUES (@id, @zooId, @name, @weight)";
                insert.Parameters.AddWithValue("@id", int.Parse(parts[0]));
                insert.Parameters.AddWithValue("@zooId", int.Parse(parts[1]));
                insert.Parameters.AddWithValue("@name", parts[2]);
                insert.Parameters.AddWithValue("@weight", double.Parse(parts[3]));
                insert.ExecuteNonQuery();
            }
        }
    }

    public List<Zoo> GetAllZoos()
    {
        var list = new List<Zoo>();

        using var c = new SqliteConnection(_cs);
        c.Open();

        var cmd = c.CreateCommand();
        cmd.CommandText = "SELECT zoo_id, zoo_name FROM zoo ORDER BY zoo_id";

        using var r = cmd.ExecuteReader();
        while (r.Read())
            list.Add(new Zoo(r.GetInt32(0), r.GetString(1)));

        return list;
    }

    public List<Animal> GetAllAnimals()
    {
        var list = new List<Animal>();

        using var c = new SqliteConnection(_cs);
        c.Open();

        var cmd = c.CreateCommand();
        cmd.CommandText = "SELECT animal_id, zoo_id, animal_name, weight FROM animal ORDER BY animal_id";

        using var r = cmd.ExecuteReader();
        while (r.Read())
            list.Add(new Animal(r.GetInt32(0), r.GetInt32(1), r.GetString(2), r.GetDouble(3)));

        return list;
    }

    public void AddAnimal(Animal a)
    {
        using var c = new SqliteConnection(_cs);
        c.Open();

        var cmd = c.CreateCommand();
        cmd.CommandText = "INSERT INTO animal(zoo_id, animal_name, weight) VALUES(@z, @n, @w)";
        cmd.Parameters.AddWithValue("@z", a.ZooId);
        cmd.Parameters.AddWithValue("@n", a.Name);
        cmd.Parameters.AddWithValue("@w", a.WeightKg);
        cmd.ExecuteNonQuery();
    }
}