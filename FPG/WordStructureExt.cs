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
}
