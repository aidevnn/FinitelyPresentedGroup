namespace FPG;

public class WordSet
{
    HashSet<Word> set;
    public WordSet(IEnumerable<Word> ws)
    {
        set = new(ws);
        Key = set.Min();
        Pairs = set.Where(w => w.weight != 0 && w.extStr.Length >= Key.extStr.Length).Select(w => (Key, w));
        Content = set;
    }
    public Word Key { get; private set; }
    public IEnumerable<(Word key, Word value)> Pairs { get; }
    public int Count => set.Count;
    public bool Overlaps(IEnumerable<Word> ws) => set.Overlaps(ws);
    public IEnumerable<Word> Content { get; }

    public void Display()
    {
        var digits = set.Max(w => w.ToString().Length);
        Console.WriteLine($"Repr : {Key}");
        foreach (var w in set.Ascending())
            Console.WriteLine($"    {w.Details(digits)}");
    }
}
