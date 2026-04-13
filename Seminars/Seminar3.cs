using System;
using System.Collections.Generic;
using System.IO;

// === Точка входа ===
string mode = args.Length > 0 ? args[0].ToLower() : "projection";

switch (mode)
{
    case "projection":
        {
            var table = ReadCsv(Console.In, ';');
            string column = args[1];
            var result = Projection(table, column);
            WriteCsv(Console.Out, result, ';');
            break;
        }

    case "where":
        {
            var table = ReadCsv(Console.In, ';');
            string column = args[1];
            string value = args[2];
            var result = Where(table, column, value);
            WriteCsv(Console.Out, result, ';');
            break;
        }

    case "join":
        {
            var left = ReadCsv(File.OpenText(args[1] + ".csv"), ';');
            var right = ReadCsv(File.OpenText(args[2] + ".csv"), ';');
            var result = Join(left, right, args[3], args[4]);
            WriteCsv(Console.Out, result, ';');
            break;
        }

    case "group_avg":
        {
            var table = ReadCsv(Console.In, ';');
            var result = GroupAvg(table, args[1], args[2]);
            WriteCsv(Console.Out, result, ';');
            break;
        }
}

// ===== CSV =====

static CsvTable ReadCsv(TextReader reader, char sep)
{
    var headers = reader.ReadLine()!.Split(sep);
    var rows = new List<CsvRow>();

    string? line;
    while ((line = reader.ReadLine()) != null)
    {
        if (string.IsNullOrWhiteSpace(line)) continue;
        rows.Add(new CsvRow(line.Split(sep)));
    }

    return new CsvTable(headers, rows);
}

static void WriteCsv(TextWriter writer, CsvTable table, char sep)
{
    writer.WriteLine(string.Join(sep, table.Headers));
    foreach (var row in table.Rows)
        writer.WriteLine(string.Join(sep, row.Fields));
}

// ===== ВСПОМОГАТЕЛЬНЫЕ =====

static int FindColumn(CsvTable t, string name)
{
    int i = Array.IndexOf(t.Headers, name);
    if (i < 0) throw new Exception("Нет колонки");
    return i;
}

// ===== ОПЕРАЦИИ =====

// Проекция
static CsvTable Projection(CsvTable t, string col)
{
    int idx = FindColumn(t, col);
    var rows = new List<CsvRow>();

    foreach (var r in t.Rows)
        rows.Add(new CsvRow(new[] { r.Fields[idx] }));

    return new CsvTable(new[] { col }, rows);
}

// Where
static CsvTable Where(CsvTable t, string col, string val)
{
    int idx = FindColumn(t, col);
    var rows = new List<CsvRow>();

    foreach (var r in t.Rows)
        if (r.Fields[idx] == val)
            rows.Add(r);

    return new CsvTable(t.Headers, rows);
}

// Join
static CsvTable Join(CsvTable a, CsvTable b, string keyA, string keyB)
{
    int iA = FindColumn(a, keyA);
    int iB = FindColumn(b, keyB);

    var headers = new List<string>();
    headers.AddRange(a.Headers);
    headers.AddRange(b.Headers);

    var rows = new List<CsvRow>();

    foreach (var ra in a.Rows)
    {
        foreach (var rb in b.Rows)
        {
            if (ra.Fields[iA] == rb.Fields[iB])
            {
                var fields = new List<string>();
                fields.AddRange(ra.Fields);
                fields.AddRange(rb.Fields);
                rows.Add(new CsvRow(fields.ToArray()));
            }
        }
    }

    return new CsvTable(headers.ToArray(), rows);
}

// Group Avg
static CsvTable GroupAvg(CsvTable t, string groupCol, string valCol)
{
    int g = FindColumn(t, groupCol);
    int v = FindColumn(t, valCol);

    var dict = new Dictionary<string, List<double>>();

    foreach (var r in t.Rows)
    {
        string key = r.Fields[g];
        double val = double.Parse(r.Fields[v]);

        if (!dict.ContainsKey(key))
            dict[key] = new List<double>();

        dict[key].Add(val);
    }

    var rows = new List<CsvRow>();

    foreach (var p in dict)
    {
        double avg = 0;
        foreach (var x in p.Value)
            avg += x;

        avg /= p.Value.Count;

        rows.Add(new CsvRow(new[] { p.Key, avg.ToString("F2") }));
    }

    return new CsvTable(new[] { groupCol, "avg_" + valCol }, rows);
}

// ===== СТРУКТУРЫ =====

record CsvRow(string[] Fields);
record CsvTable(string[] Headers, List<CsvRow> Rows);