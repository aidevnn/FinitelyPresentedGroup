namespace FPG;

public static class WordStructureExt
{
    public static WordSet RewriteSet(this WordStructure wstr, WordSet ws)
    {
        HashSet<Word> ws0 = new();
        foreach (var w in ws)
            ws0.Add(wstr.ReduceWord(w));

        return new(ws0);
    }
    public static WordSet RewriteSelfSet(this WordStructure wstr, WordSet ws)
    {
        HashSet<Word> ws0 = new(ws);
        foreach (var w in ws)
            ws0.Add(wstr.ReduceWord(w));

        return new(ws0);
    }
    public static WordStructure RewriteSelf(this WordStructure wstr)
    {
        var wstr0 = new WordStructure();
        foreach (var ws in wstr)
        {
            var ws0 = wstr.RewriteSelfSet(ws);
            wstr0 = new WordStructure(ws0, wstr0);
        }

        return wstr0;
    }
    public static WordStructure RewriteStruct(this WordStructure wstr, WordStructure other)
    {
        var wstr0 = new WordStructure(wstr);
        foreach (var ws in other)
        {
            var ws0 = wstr.RewriteSet(ws);
            wstr0 = new WordStructure(ws0, wstr0);
        }

        return wstr0;
    }
    public static WordSet Product(this WordStructure wstr, WordSet ws0, WordSet ws1)
    {
        HashSet<Word> ws2 = new();
        foreach (var w0 in ws0)
        {
            var wi0 = w0.Invert();
            foreach (var w1 in ws1)
            {
                var w2 = wi0.Add(w1);
                ws2.Add(w2);
            }
        }

        return new(ws2.Select(w => wstr.ReduceWord(w)));
    }
    public static WordStructure DevelopProduct(this WordStructure wstr)
    {
        var wstr0 = new WordStructure(wstr);
        List<WordSet> sets = new();
        foreach (var ws0 in wstr)
            foreach (var ws1 in wstr)
                sets.Add(wstr.Product(ws0, ws1));

        foreach (var ws in sets)
            wstr0 = new WordStructure(ws, wstr0);

        return wstr0;
    }
    public static WordStructure LoopDevelop(this WordStructure wstr0, int loopMax = 5)
    {
        var wstr = new WordStructure(wstr0);
        for (int k = 0; k < loopMax; ++k)
        {
            int sz0 = wstr.TotalWords;
            wstr = wstr.DevelopProduct();
            if (sz0 == wstr.TotalWords)
                break;
        }
        return wstr.RewriteSelf();
    }
    public static void IsGroup(this WordStructure wordStructure)
    {
        var keys = wordStructure.Select(ws => ws.Key).ToHashSet();
        var table = new Word[keys.Count, keys.Count];
        var idx = Enumerable.Range(0, keys.Count).ToArray();
        var arr = wordStructure.Select(ws => ws.Key).Ascending().ToArray();
        for (int i = 0; i < idx.Length; ++i)
        {
            var e0 = arr[i];
            for (int j = 0; j < idx.Length; ++j)
            {
                var e1 = arr[j];
                table[i, j] = wordStructure.ReduceWord(e0.Add(e1));
            }
        }

        var rows = idx.Select(i => idx.Select(j => table[i, j]));
        var cols = idx.Select(j => idx.Select(i => table[i, j]));
        var isGroup = rows.All(keys.SetEquals) && cols.All(keys.SetEquals);
        var isComm = idx.SelectMany(i => idx.Select(j => (i, j))).All(e => table[e.i, e.j].Equals(table[e.j, e.i]));

        Console.WriteLine($"Is Group   : {isGroup}");
        Console.WriteLine($"Is Abelian : {isComm}");
        Console.WriteLine();

        // Verifying group and abelian
        // Console.WriteLine(keys.Ascending().Glue(","));
        // foreach (var r in rows) Console.WriteLine(r.Ascending().Glue(","));
        // foreach (var c in cols) Console.WriteLine(c.Ascending().Glue(","));
        // Console.WriteLine();
        return;
    }
    public static void GroupTable(this WordStructure wordStructure)
    {
        var keys = wordStructure.Select(ws => ws.Key).Ascending().ToList();
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
                row.Add(wordStructure.ReduceWord(w0.Add(w1)));

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
