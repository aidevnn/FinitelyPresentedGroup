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
        RegexList = Pairs.Where(a => !a.key.Equals(a.value)).GroupBy(a => a.key).ToDictionary(b => new Regex(b.Select(w => w.value.extStr).Glue("|")), a => a.Key.extStr);
    }

    public WordStructure(WordSet ws)
    {
        sets = new() { ws };
        var Pairs = sets.SelectMany(ws => ws.Pairs).OrderByDescending(a => a.value.extStr.Length - a.key.extStr.Length).ThenByDescending(a => a.value);
        RegexList = Pairs.Where(a => !a.key.Equals(a.value)).GroupBy(a => a.key).ToDictionary(b => new Regex(b.Select(w => w.value.extStr).Glue("|")), a => a.Key.extStr);
    }

    public WordStructure(WordStructure wstr)
    {
        sets = wstr.sets.ToList();
        var Pairs = sets.SelectMany(ws => ws.Pairs).OrderByDescending(a => a.value.extStr.Length - a.key.extStr.Length).ThenByDescending(a => a.value);
        RegexList = Pairs.Where(a => !a.key.Equals(a.value)).GroupBy(a => a.key).ToDictionary(b => new Regex(b.Select(w => w.value.extStr).Glue("|")), a => a.Key.extStr);
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
        var Pairs = sets.SelectMany(ws => ws.Pairs).OrderByDescending(a => a.value.extStr.Length - a.key.extStr.Length).ThenByDescending(a => a.value);
        RegexList = Pairs.Where(a => !a.key.Equals(a.value)).GroupBy(a => a.key).ToDictionary(b => new Regex(b.Select(w => w.value.extStr).Glue("|")), a => a.Key.extStr);
    }

    static string LoopReduce(string w0, string w1, Regex rg)
    {
        int sz = 0;
        while (sz != w0.Length)
        {
            sz = w0.Length;
            w0 = rg.Replace(w0, w1);
        }

        return w0.Reduce();
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
                var s = LoopReduce(wi.extStr, rg.Value, rg.Key);
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
