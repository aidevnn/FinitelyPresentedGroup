namespace FPG;

public static class StringExt
{
    public static string JoinChars(this IEnumerable<char> cs) => string.Join("", cs);
    public static bool AreInvert(char c0, char c1)
    {
        var d = c0 - c1;
        return d == 32 || d == -32;
    }

    public static bool AreInvert(string s0, string s1) => s0.Length == s1.Length && s0.Zip(s1).All(e => AreInvert(e.First, e.Second));

    public static string Reduce(this string word)
    {
        Stack<char> stack = new Stack<char>(30);
        foreach (var c in word)
        {
            if (stack.Count == 0)
            {
                stack.Push(c);
                continue;
            }
            else if (AreInvert(c, stack.Peek()))
                stack.Pop();
            else
                stack.Push(c);
        }

        return new String(stack.Reverse().ToArray());
    }

    public static string Reduce(this string word, string pattern, string substitute)
    {
        var nword = word;
        while (true)
        {
            int sz0 = nword.Length;
            nword = nword.Replace(pattern, substitute).Reduce();
            int sz1 = nword.Length;
            if (sz0 == sz1)
                break;
        }

        return nword;
    }

    public static string Reduce(this string word, string pattern) => word.Reduce(pattern, "");

    static IComparer<string> StrComp = Comparer<string>.Create((s0, s1) =>
    {
        var compL = s0.Length.CompareTo(s1.Length);
        if (compL != 0)
            return compL;

        return s0.CompareTo(s1);
    });

    public static int StrComparison(this string s0, string s1) => StrComp.Compare(s0, s1);

}

