using System.Collections;

namespace FPG;

public class WordSet : IEnumerable<Word>
{
    HashSet<Word> set;
    public WordSet(IEnumerable<Word> ws)
    {
        set = new(ws);
        Key = set.Min();
    }
    public Word Key { get; private set; }
    public int Count => set.Count;
    public bool Overlaps(IEnumerable<Word> ws) => set.Overlaps(ws);

    public IEnumerator<Word> GetEnumerator() => set.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => set.GetEnumerator();
    public void Display()
    {
        var digits = set.Max(w => w.ToString().Length);
        Console.WriteLine($"Repr : {Key.extStr2}");
        foreach (var w in set.Ascending())
            Console.WriteLine($"    {w.Details(digits)}");
    }

}
