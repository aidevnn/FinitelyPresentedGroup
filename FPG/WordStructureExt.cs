using System.Diagnostics;
namespace FPG;

public static class WordStructureExt
{
    public static Word Rewrite(this WordStructure wstr, Word w)
    {
        var wi = w;
        var pairs = wstr.Pairs();
        WordSet ws0 = new(pairs.Count());
        while (true)
        {
            var s0 = wi.GetHashCode();
            foreach (var ws1 in pairs)
            {
                var substitute = ws1.key;
                var pattern = ws1.value;
                if (pattern.Equals(substitute)) continue;
                var w0 = wi.extStr.Reduce(pattern.extStr, substitute.extStr);
                ws0.Add(new Word(w0));
            }

            var wf = ws0.Min();
            if (s0 == wf.GetHashCode())
            {
                // Console.WriteLine($"{wi.extStr} => {wf.extStr}");
                return wf;
            }
            ws0.Clear();
            wi = wf;
        }
    }
    public static WordSet Rewrite(this WordStructure wstr, WordSet ws)
    {
        // var ws0 = new WordSet(ws);
        var ws0 = new WordSet();
        foreach (var w in ws)
            ws0.Add(wstr.Rewrite(w));

        return ws0;
    }
    public static WordStructure Rewrite(this WordStructure wstr, WordStructure other)
    {
        var wstr0 = new WordStructure(wstr);
        foreach (var ws in other.WSets())
        {
            var ws0 = wstr.Rewrite(ws);
            wstr0 = new WordStructure(ws0, wstr0);
        }

        return wstr0;
    }
    public static WordSet Product(this WordStructure wstr, WordSet ws0, WordSet ws1)
    {
        var ws2 = new WordSet();
        foreach (var w0 in ws0)
        {
            var wi0 = w0.Invert();
            foreach (var w1 in ws1)
            {
                var w2 = new Word(wi0.extStr + w1.extStr);
                ws2.Add(wstr.Rewrite(w2));
            }
        }

        // return wstr.Rewrite(ws2);
        return ws2;
    }

    static Stopwatch sw = new Stopwatch();
    public static int loop = 0;
    public static WordStructure Develop(this WordStructure wstr)
    {
        sw.Restart();
        var wsets = wstr.WSets();
        // Console.WriteLine(wsets.Sum(ws => ws.Count));
        var wstr0 = new WordStructure(wstr);
        List<WordSet> sets = new();
        foreach (var ws0 in wsets)
        {
            foreach (var ws1 in wsets)
            {
                sets.Add(wstr.Product(ws0, ws1));
            }
        }

        foreach (var ws in sets)
            wstr0 = new WordStructure(ws, wstr0);

        Console.WriteLine($"Loop:{++loop,3}; Time:{sw.ElapsedMilliseconds} ms");
        Console.WriteLine();
        return wstr0;
    }

    public static void GroupTable(this WordStructure wordStructure)
    {
        var keys = wordStructure.WSets().Select(ws => ws.Key).Ascending().ToList();
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
                var w3 = wordStructure.Rewrite(w2);
                row.Add(w3);
            }
            var rowStr = string.Format("{0} | {1}", string.Format(fmt, w0.extStr2), row.Select(w => w.extStr2).Glue(" ", fmt));
            Console.WriteLine(rowStr);
        }
    }
}
