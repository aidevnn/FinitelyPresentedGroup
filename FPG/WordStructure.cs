using System.Collections;
using System.Text.RegularExpressions;

namespace FPG;

public class WordStructure : IEnumerable<WordSet>
{
    List<WordSet> sets;
    Dictionary<string, Regex> RegexList { get; }
    public WordStructure()
    {
        sets = new();
        var Pairs = Enumerable.Empty<(Word key, Word value)>();
        RegexList = new Dictionary<string, Regex>();
    }

    public WordStructure(WordSet ws)
    {
        sets = new() { ws };
        var ordSets = sets.Where(ws => ws.Count != 1).OrderByDescending(ws => ws.MaxWord.weight - ws.Key.weight).ThenByDescending(ws => ws.MaxWord);
        RegexList = ordSets.ToDictionary(ws => ws.Key.extStr, ws => new Regex(ws.RegExPattern));
    }
    public WordStructure(WordStructure wstr)
    {
        sets = wstr.sets.ToList();
        var ordSets = sets.Where(ws => ws.Count != 1).OrderByDescending(ws => ws.MaxWord.weight - ws.Key.weight).ThenByDescending(ws => ws.MaxWord);
        RegexList = ordSets.ToDictionary(ws => ws.Key.extStr, ws => new Regex(ws.RegExPattern));
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
        var ordSets = sets.Where(ws => ws.Count != 1).OrderByDescending(ws => ws.MaxWord.weight - ws.Key.weight).ThenByDescending(ws => ws.MaxWord);
        RegexList = ordSets.ToDictionary(ws => ws.Key.extStr, ws => new Regex(ws.RegExPattern));
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
                var s = wi.extStr.LoopReduce(rg.Key, rg.Value);
                set.Add(s);
            }

            wi = set.Select(s => new Word(s.ParseExtendedWord())).Min();
            set.Clear();
        }

        return wi;
    }

    public int TotalWords => sets.Sum(ws => ws.Count);

    public void Display()
    {
        foreach (var ws in sets.OrderBy(a => a.Key).ThenBy(a => a.Count))
            ws.Display();

        Console.WriteLine();
        Console.WriteLine($"Total Words : {TotalWords}");
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

    public IEnumerator<WordSet> GetEnumerator() => sets.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => sets.GetEnumerator();
}
