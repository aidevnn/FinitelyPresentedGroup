using System.Collections;

namespace FPG;

public struct Word : IEnumerable<Letter>, IEquatable<Word>, IComparable<Word>
{
    public static int count = 0;
    public static Word Empty => new Word();
    IEnumerable<Letter> letters;
    public Word()
    {
        letters = Enumerable.Empty<Letter>();
        weight = length = 0;
        extStr = "";
    }
    public Word(string word)
    {
        letters = word.ToLetters();
        length = letters.Count();
        weight = letters.Sum(l => l.weight);
        extStr = new String(letters.SelectMany(l => l.Extend()).ToArray());
        ++Word.count;
    }
    private Word(Word w)
    {
        letters = w.Select(l => l.Invert()).Reverse();
        length = w.length;
        weight = w.weight;
        extStr = new String(letters.SelectMany(l => l.Extend()).ToArray());
    }
    public Word Invert() => new Word(this);
    public int length { get; }
    public int weight { get; }
    public string extStr { get; }
    public string extStr2 => length == 0 ? "()" : extStr;

    public IEnumerator<Letter> GetEnumerator() => letters.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => letters.GetEnumerator();

    public bool Equals(Word other) => string.Equals(extStr, other.extStr);
    public int CompareTo(Word other)
    {
        var compL = length.CompareTo(other.length);
        if (compL != 0)
            return compL;

        var compW = weight.CompareTo(other.weight);
        if (compW != 0)
            return compW;

        return this.SequenceCompare(other);
    }

    public string Details(int digits = 2)
    {
        var fmt = $"{{0,-{digits}}} => {extStr2}";
        return string.Format(fmt, this);
    }

    public override string ToString() => length == 0 ? "()" : letters.Glue();
    public override int GetHashCode() => extStr.GetHashCode();
}
