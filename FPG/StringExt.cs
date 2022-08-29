using System.Text.RegularExpressions;
namespace FPG;

public static class StringExt
{
    static Regex ReduceRgx { get; }
    static StringExt()
    {
        var alph = "abcdefghijkl";
        var pattern = alph.Select(c => (c, char.ToUpper(c))).Select(t => $"{t.c}{t.Item2}|{t.Item2}{t.c}").Glue("|");
        ReduceRgx = new Regex(pattern);
    }
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

    // public static string Reduce(this string word)
    // {
    //     int sz = 0;
    //     while (word.Length != sz)
    //     {
    //         sz = word.Length;
    //         word = ReduceRgx.Replace(word, "");
    //     }

    //     return word;
    // }

}

