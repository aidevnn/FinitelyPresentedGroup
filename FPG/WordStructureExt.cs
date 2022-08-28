using System.Diagnostics;
namespace FPG;

public static class WordStructureExt
{
    public static int count = 0;
    public static Word RewriteWord(this WordStructure wstr, Word w)
    {
        var wi = w;
        int s0 = 0;
        var pairs = wstr.Pairs;

        HashSet<string> set = new(pairs.Count());
        do
        {
            s0 = wi.GetHashCode();
            set.Add(wi.extStr);
            foreach (var kv in pairs)
            {
                var substitute = kv.key;
                var pattern = kv.value;
                if (pattern.Equals(substitute)) continue;
                var w0 = wi.extStr.Reduce(pattern.extStr, substitute.extStr);
                set.Add(w0);
            }

            wi = set.Select(w => new Word(w)).Min();
            set.Clear();
        } while (s0 != wi.GetHashCode());

        return wi;
    }
    public static WordSet RewriteSet(this WordStructure wstr, WordSet ws)
    {
        HashSet<Word> ws0 = new();
        foreach (var w in ws.Content)
            ws0.Add(wstr.RewriteWord(w));

        return new(ws0);
    }
    public static WordStructure RewriteStruct(this WordStructure wstr, WordStructure other)
    {
        var wstr0 = new WordStructure(wstr);
        foreach (var ws in other.WSets())
        {
            var ws0 = wstr.RewriteSet(ws);
            wstr0 = new WordStructure(ws0, wstr0);
        }

        return wstr0;
    }
    public static WordSet Product(this WordStructure wstr, WordSet ws0, WordSet ws1)
    {
        HashSet<Word> ws2 = new();
        foreach (var w0 in ws0.Content)
        {
            var wi0 = w0.Invert();
            foreach (var w1 in ws1.Content)
            {
                var w2 = new Word(wi0.extStr + w1.extStr);
                ws2.Add(w2);
            }
        }

        return new(ws2.Select(w => wstr.RewriteWord(w)));
    }

    public static WordStructure Develop(this WordStructure wstr)
    {
        var sw = Stopwatch.StartNew();
        var wsets = wstr.WSets();
        var wstr0 = new WordStructure(wstr);
        List<WordSet> sets = new();
        foreach (var ws0 in wsets)
            foreach (var ws1 in wsets)
                sets.Add(wstr.Product(ws0, ws1));

        foreach (var ws in sets)
            wstr0 = new WordStructure(ws, wstr0);

        // Console.WriteLine($"Loop Time:{sw.ElapsedMilliseconds} ms");
        return wstr0;
    }
    public static void IsGroup(this WordStructure wordStructure)
    {
        var keys = wordStructure.WSets().Select(ws => ws.Key).Ascending().ToHashSet();
        var isComm = true;
        foreach (var e0 in keys)
        {
            var ei0 = e0.Invert();
            foreach (var e1 in keys)
            {
                var e2 = wordStructure.RewriteWord(new Word(ei0.extStr + e1.extStr));
                if (isComm)
                {
                    var e3 = wordStructure.RewriteWord(new Word(e1.extStr + ei0.extStr));
                    isComm &= e2.Equals(e3);
                }

                if (!keys.Contains(e2))
                {
                    Console.WriteLine("Is Group   : False");
                    Console.WriteLine("Is Abelian : False");
                    Console.WriteLine();
                    return;
                }
            }
        }

        Console.WriteLine("Is Group   : True");
        Console.WriteLine($"Is Abelian : {isComm}");
        Console.WriteLine();
        return;
    }
    public static void GroupTable(this WordStructure wordStructure)
    {
        var keys = wordStructure.WSets().Select(ws => ws.Key).Ascending().ToList();
        if (keys.Count > 30)
        {
            Console.WriteLine("*** TOO HUGE ***");
            return;
        }

        var digits = keys.Max(w => w.extStr.Length);
        var fmt = $"{{0,{digits}}}";
        var head = string.Format("{0} | {1}", string.Format(fmt, "()"), keys.Skip(1).Select(w => w.extStr2).Glue(" ", fmt));
        Console.WriteLine(head);
        Console.WriteLine(Enumerable.Repeat('-', head.Length).Glue());
        List<string> rows = new();
        foreach (var w0 in keys.Skip(1))
        {
            List<Word> row = new();
            foreach (var w1 in keys.Skip(1))
            {
                var w2 = new Word(w0.extStr + w1.extStr);
                var w3 = wordStructure.RewriteWord(w2);
                row.Add(w3);
            }

            var rowStr = string.Format("{0} | {1}", string.Format(fmt, w0.extStr2), row.Select(w => w.extStr2).Glue(" ", fmt));
            Console.WriteLine(rowStr);
        }
    }
}
