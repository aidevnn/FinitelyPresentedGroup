namespace FPG;

public static class WordStructureExt
{
    public static WordSet RewriteSet(this WordStructure wstr, WordSet ws)
    {
        HashSet<Word> set = new();
        foreach (var w in ws)
            set.Add(wstr.ReduceWord(w));

        return new(set);
    }
    public static WordSet RewriteSelfSet(this WordStructure wstr, WordSet ws)
    {
        HashSet<Word> set = new(ws);
        foreach (var w in ws)
            set.Add(wstr.ReduceWord(w));

        return new(set);
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
        HashSet<Word> set = new();
        foreach (var w0 in ws0)
        {
            var wi0 = w0.Invert();
            foreach (var w1 in ws1)
            {
                var w2 = wi0.Add(w1);
                set.Add(w2);
            }
        }

        return new(set.Select(w => wstr.ReduceWord(w)));
    }
    public static WordStructure ExpandProduct(this WordStructure wstr, WordSet ws)
    {
        var wstr0 = new WordStructure(wstr);
        List<WordSet> sets = new();
        foreach (var ws0 in wstr)
            sets.Add(wstr.Product(ws0, ws));

        foreach (var ws0 in sets)
            wstr0 = new WordStructure(ws0, wstr0);

        return wstr0;
    }
    public static WordStructure DevelopProduct(this WordStructure wstr)
    {
        var words = wstr.Select(ws => ws.Key).ToHashSet();
        List<WordSet> sets = new();
        foreach (var ws0 in wstr)
        {
            var w2 = wstr.ReduceWord(ws0.Key.Add(ws0.Key));
            if (!words.Contains(w2))
            {
                sets.Add(ws0);
                continue;
            }

            var wi0 = ws0.Key.Invert();
            foreach (var ws1 in wstr)
            {
                var wi2 = wstr.ReduceWord(wi0.Add(ws1.Key));
                if (!words.Contains(wi2))
                {
                    sets.Add(ws0);
                    break;
                }
            }
        }

        var wstr0 = new WordStructure(wstr);
        foreach (var ws in sets)
            wstr0 = wstr0.ExpandProduct(ws);

        return wstr0;
    }
    public static WordStructure LoopDevelop(this WordStructure wstr, int loopMax = 5)
    {
        var wstr0 = new WordStructure(wstr);
        for (int k = 0; k < loopMax; ++k)
        {
            int sz0 = wstr0.TotalWords;
            wstr0 = wstr0.DevelopProduct().RewriteSelf();
            if (sz0 == wstr0.TotalWords)
                break;
        }
        return wstr0;
    }
}
