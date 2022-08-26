namespace FPG;

public static class EnumerableExt
{
    public static string Glue<U>(this IEnumerable<U> us, string sep = "", string fmt = "{0}")
    {
        return string.Join(sep, us.Select(u => string.Format(fmt, u)));
    }
    public static IOrderedEnumerable<U> Ascending<U>(this IEnumerable<U> us) where U : IComparable<U>
    {
        return us.OrderBy(a => a);
    }
    public static IOrderedEnumerable<U> Descending<U>(this IEnumerable<U> us) where U : IComparable<U>
    {
        return us.OrderByDescending(a => a);
    }

    // public static int SequenceCompare<U>(this IEnumerable<U> us, IEnumerable<U> other) where U : IComparable<U>
    // {
    //     return us.Zip(other, (e0, e1) => e0.CompareTo(e1)).FirstOrDefault(i => i != 0, us.Count().CompareTo(other.Count()));
    // }

    public static int SequenceCompare<U>(this IEnumerable<U> us, IEnumerable<U> other) where U : IComparable<U>
    {
        var e0 = us.GetEnumerator();
        var e1 = other.GetEnumerator();
        while (e0.MoveNext() && e1.MoveNext())
        {
            var comp = e0.Current.CompareTo(e1.Current);
            if (comp != 0)
                return comp;
        }

        return us.Count().CompareTo(other.Count());
    }
}
