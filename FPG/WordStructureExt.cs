using System.Diagnostics;
namespace FPG;

public static class WordStructureExt
{
    public static int count = 0;
    public static WordSet RewriteSet(this WordStructure wstr, WordSet ws)
    {
        HashSet<Word> ws0 = new();
        foreach (var w in ws.Content)
            ws0.Add(wstr.ReduceWord(w));

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

        return new(ws2.Select(w => wstr.ReduceWord(w)));
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

        // Console.WriteLine($"Loop Time:{sw.ElapsedMilliseconds} ms; Words : {wstr0.Count}");
        return wstr0;
    }

    public static WordStructure LoopDevelop(this WordStructure wstr0, int loopMax = 5)
    {
        var wstr = new WordStructure(wstr0);
        for (int k = 0; k < loopMax; ++k)
        {
            int sz0 = wstr.Count;
            wstr = wstr.Develop();
            if (sz0 == wstr.Count)
                break;
        }
        return wstr;
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
                var e2 = wordStructure.ReduceWord(new Word(ei0.extStr + e1.extStr));
                if (isComm)
                {
                    var e3 = wordStructure.ReduceWord(new Word(e1.extStr + ei0.extStr));
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
                var w3 = wordStructure.ReduceWord(w2);
                row.Add(w3);
            }

            var rowStr = string.Format("{0} | {1}", string.Format(fmt, w0.extStr2), row.Select(w => w.extStr2).Glue(" ", fmt));
            Console.WriteLine(rowStr);
        }
    }

    public static WordStructure MergeRelation(params string[] relations)
    {
        if (relations.Length == 0)
            return new WordStructure();

        var nbGens = relations.SelectMany(r => r).Where(c => char.IsLetter(c)).Select(c => char.ToLower(c)).Distinct().Count();
        if (nbGens > relations.Count())
            return new WordStructure();

        Queue<string> rels = new(relations);
        var wstr = Relation.Structure(rels.Dequeue());
        while (rels.Count != 0)
            wstr = wstr.RewriteStruct(Relation.Structure(rels.Dequeue()));

        return wstr;
    }
}
