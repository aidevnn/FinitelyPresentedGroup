using System.Collections;

namespace FPG;

public class WordSet : IEnumerable<Word>
{
    HashSet<Word> set;
    public WordSet(IEnumerable<Word> ws)
    {
        set = new(ws);
        Key = set.Min();
        MaxWord = set.Max();
    }
    public WordSet(Word w) : this(new[] { w }) { }
    public Word Key { get; private set; }
    public Word MaxWord { get; }
    public int Count => set.Count;
    public bool Overlaps(IEnumerable<Word> ws) => set.Overlaps(ws);
    public string RegExPattern => this.Descending().Where(w => !w.Equals(Key)).Select(w => w.extStr).Glue("|");

    public IEnumerator<Word> GetEnumerator() => set.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => set.GetEnumerator();
    public void Display(int digits = 1)
    {
        var fmt = $"{{0,-{digits}}}";
        var reprs = string.Format(fmt, Key.extStr2);
        Console.WriteLine($"    {reprs} => {{ {this.Ascending().Select(w => w.extStr2).Glue(", ")} }}");
    }
}
