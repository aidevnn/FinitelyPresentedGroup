namespace FPG;

public class WordStructure
{
    List<WordSet> sets;
    public WordStructure()
    {
        sets = new();
    }

    public WordStructure(WordSet ws)
    {
        sets = new() { ws };
    }

    public WordStructure(WordStructure wstr)
    {
        sets = wstr.sets.Select(w => new WordSet(w)).ToList();
    }

    public WordStructure(WordSet ws, WordStructure wstr)
    {
        var merged = new WordSet(ws);
        sets = new() { merged };
        foreach (var ws0 in wstr.sets)
        {
            if (ws0.Overlaps(merged))
                merged.UnionWith(ws0);
            else
                sets.Add(ws0);
        }
    }

    public int Count => sets.Sum(ws => ws.Count);
    public IEnumerable<WordSet> WSets() => sets;
    public IEnumerable<(Word key, Word value)> Pairs()
    {
        return sets.SelectMany(ws => ws.Pairs()).OrderByDescending(a => a.value.extStr.Length - a.key.extStr.Length).ThenByDescending(a => a.value);
    }

    public void Display()
    {
        foreach (var ws in sets.OrderBy(a => a.Key).ThenBy(a => a.Count))
            ws.Display();

        Console.WriteLine();
    }

    public void DisplayKeys()
    {
        var digits = sets.Select(ws => ws.Key).Select(w => w.ToString().Length).Max();
        foreach (var ws in sets.OrderBy(a => a.Key).ThenBy(a => a.Count))
            Console.WriteLine(ws.Key.Details(digits));

        Console.WriteLine(sets.Select(a => a.Key).Ascending().Glue(", "));
        Console.WriteLine($"Total : {sets.Count}");
        Console.WriteLine();
    }
}
