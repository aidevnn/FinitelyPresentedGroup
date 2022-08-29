using System.Text.RegularExpressions;

namespace FPG;

public class WordStructure
{
    List<WordSet> sets;
    Dictionary<Regex, string> RegexList { get; }
    public WordStructure()
    {
        sets = new();
        var Pairs = Enumerable.Empty<(Word key, Word value)>();
        RegexList = new Dictionary<Regex, string>();
    }

    public WordStructure(WordSet ws)
    {
        sets = new() { ws };
        var Pairs = sets.SelectMany(ws => ws.Select(w => (key: ws.Key, value: w))).OrderByDescending(a => a.value.extStr.Length - a.key.extStr.Length).ThenByDescending(a => a.value);
        RegexList = Pairs.Where(a => !a.key.Equals(a.value)).GroupBy(a => a.key).ToDictionary(b => new Regex(b.Select(w => w.value.extStr).Glue("|")), a => a.Key.extStr);
        // RegexList = sets.ToDictionary(ws => new Regex(ws.Content.Where(w => !w.Equals(ws.Key)).Select(w => w.extStr).Descending().Glue("|")), ws => ws.Key.extStr);
    }

    public WordStructure(WordStructure wstr)
    {
        sets = wstr.sets.ToList();
        var Pairs = sets.SelectMany(ws => ws.Select(w => (key: ws.Key, value: w))).OrderByDescending(a => a.value.extStr.Length - a.key.extStr.Length).ThenByDescending(a => a.value);
        RegexList = Pairs.Where(a => !a.key.Equals(a.value)).GroupBy(a => a.key).ToDictionary(b => new Regex(b.Select(w => w.value.extStr).Glue("|")), a => a.Key.extStr);
        // RegexList = sets.ToDictionary(ws => new Regex(ws.Content.Where(w => !w.Equals(ws.Key)).Select(w => w.extStr).Descending().Glue("|")), ws => ws.Key.extStr);
    }

    public WordStructure(WordSet ws, WordStructure wstr)
    {
        var merged = new HashSet<Word>(ws);
        sets = new();
        foreach (var ws0 in wstr.sets)
        {
            if (ws0.Overlaps(merged))
                merged.UnionWith(ws0);
            else
                sets.Add(ws0);
        }
        sets.Add(new WordSet(merged));
        var Pairs = sets.SelectMany(ws => ws.Select(w => (key: ws.Key, value: w))).OrderByDescending(a => a.value.extStr.Length - a.key.extStr.Length).ThenByDescending(a => a.value);
        RegexList = Pairs.Where(a => !a.key.Equals(a.value)).GroupBy(a => a.key).ToDictionary(b => new Regex(b.Select(w => w.value.extStr).Glue("|")), a => a.Key.extStr);
        // RegexList = sets.ToDictionary(ws => new Regex(ws.Content.Where(w => !w.Equals(ws.Key)).Select(w => w.extStr).Descending().Glue("|")), ws => ws.Key.extStr);
    }

    public Word ReduceWord(Word w)
    {
        var wi = w;
        int hash = 0;
        HashSet<string> set = new();
        while (wi.extStr.GetHashCode() != hash)
        {
            hash = wi.extStr.GetHashCode();
            set.Add(wi.extStr);
            foreach (var rg in RegexList)
            {
                var s = wi.extStr.LoopReduce(rg.Value, rg.Key);
                set.Add(s);
            }

            wi = set.Select(s => new Word(s)).Min();
            set.Clear();
        }

        return wi;
    }

    public int Count => sets.Sum(ws => ws.Count);
    public IEnumerable<WordSet> WSets() => sets;

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
