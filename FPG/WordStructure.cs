namespace FPG;

public class WordStructure
{
    List<WordSet> sets;
    public WordStructure()
    {
        sets = new();
        Pairs = Enumerable.Empty<(Word key, Word value)>();
    }

    public WordStructure(WordSet ws)
    {
        sets = new() { ws };
        Pairs = sets.SelectMany(ws => ws.Pairs).OrderByDescending(a => a.value.extStr.Length - a.key.extStr.Length).ThenByDescending(a => a.value);
    }

    public WordStructure(WordStructure wstr)
    {
        sets = wstr.sets.ToList();
        Pairs = sets.SelectMany(ws => ws.Pairs).OrderByDescending(a => a.value.extStr.Length - a.key.extStr.Length).ThenByDescending(a => a.value);
    }

    public WordStructure(WordSet ws, WordStructure wstr)
    {
        var merged = new HashSet<Word>(ws.Content);
        sets = new();
        foreach (var ws0 in wstr.sets)
        {
            if (ws0.Overlaps(merged))
                merged.UnionWith(ws0.Content);
            else
                sets.Add(ws0);
        }
        sets.Add(new WordSet(merged));
        Pairs = sets.SelectMany(ws => ws.Pairs).OrderByDescending(a => a.value.extStr.Length - a.key.extStr.Length).ThenByDescending(a => a.value);
    }

    public int Count => sets.Sum(ws => ws.Count);
    public IEnumerable<WordSet> WSets() => sets;
    public IEnumerable<(Word key, Word value)> Pairs { get; }

    public void Display()
    {
        foreach (var ws in sets.OrderBy(a => a.Key).ThenBy(a => a.Count))
            ws.Display();

        Console.WriteLine();
        Console.WriteLine($"Total Words : {Count}");
    }

    public void DisplayReprs()
    {
        Console.WriteLine();
        Console.WriteLine("G = {{ {0} }}", sets.Select(a => a.Key).Ascending().Glue(", "));

        var digits = sets.Select(ws => ws.Key).Select(w => w.ToString().Length).Max();
        foreach (var ws in sets.OrderBy(a => a.Key).ThenBy(a => a.Count))
            Console.WriteLine(ws.Key.Details(digits));

        Console.WriteLine();
        Console.WriteLine($"Order = {sets.Count}");
    }
}
