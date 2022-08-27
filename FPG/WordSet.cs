namespace FPG;

public class WordSet : HashSet<Word>
{
    public WordSet() { }
    public WordSet(int capacity) : base(capacity) { }
    public WordSet(IEnumerable<Word> w) : base(w) { }
    public Word Key => this.Min();
    public IEnumerable<(Word key, Word value)> Pairs()
    {
        var key = Key;
        return this.Where(w => w.weight != 0 && w.extStr.Length >= key.extStr.Length).Select(w => (key, w));
    }

    public void Display()
    {
        var digits = this.Max(w => w.ToString().Length);
        Console.WriteLine($"Repr : {Key}");
        foreach (var w in this.Ascending())
            Console.WriteLine($"    {w.Details(digits)}");
    }
}
