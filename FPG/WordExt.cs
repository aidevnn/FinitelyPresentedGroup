namespace FPG;

public static class WordExt
{
    public static int Compare(Word x, Word y)
    {
        var compP = x.Count(l => l.pow < 0).CompareTo(y.Count(l => l.pow < 0));
        if (compP != 0)
            return compP;

        var compW = x.weight.CompareTo(y.weight);
        if (compW != 0)
            return compW;

        return x.extStr.CompareTo(y.extStr);
        // return x.SequenceCompare(y);
    }

    public static Comparer<Word> Comparer = Comparer<Word>.Create((x, y) => Compare(x, y));
}
