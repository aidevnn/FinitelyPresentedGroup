using System.Diagnostics;

namespace FPG;

public class WordGroup
{
    public WordGroup(params string[] relations)
    {
        Structure = new();
        foreach (var r in relations)
            Structure = Structure.RewriteStruct(Relation.Structure(r));

        Structure = Structure.LoopDevelop();

        var list1 = Structure.Where(ws => !ws.Contains(Word.Empty)).Select(ws => ws.Where(w => w.length == 1)).Where(lw => lw.Count() != 0).Select(lw => lw.Select(w => w.First()));
        var list2 = Structure.SelectMany(ws => ws.SelectMany(w => w.Select(l => l))).Distinct().Where(l => l.pow < 0 && list1.Any(l0 => l0.Contains(l)));
        var list3 = list2.Select(l => (l, lt: list1.First(l0 => l0.Contains(l)))).Select(p => (p.l, s: p.lt.First(l => l.c == p.l.c && l.pow > 0)));

        Substitution = list3.ToDictionary(a => a.l, a => a.s);
        Elements = Structure.Select(ws => Substitute(ws.Key)).Ascending().ToArray();
    }

    WordStructure Structure { get; }
    Word[] Elements { get; }
    Dictionary<Letter, Letter> Substitution { get; }
    Word Substitute(Word w) => new Word(w.AlwaysPositive(Substitution));
    public void DisplayElements()
    {
        var keys = Elements.ToHashSet();
        var table = new Word[keys.Count, keys.Count];
        var idx = Enumerable.Range(0, keys.Count).ToArray();
        for (int i = 0; i < idx.Length; ++i)
        {
            var e0 = Elements[i];
            for (int j = 0; j < idx.Length; ++j)
            {
                var e1 = Elements[j];
                var e2 = Structure.ReduceWord(e0.Add(e1));
                table[i, j] = Substitute(e2);
            }
        }

        var rows = idx.Select(i => idx.Select(j => table[i, j]));
        var cols = idx.Select(j => idx.Select(i => table[i, j]));
        var isGroup = rows.All(keys.SetEquals) && cols.All(keys.SetEquals);
        var isComm = idx.SelectMany(i => idx.Select(j => (i, j))).All(e => table[e.i, e.j].Equals(table[e.j, e.i]));

        // // Verifying rows and columns
        // Console.WriteLine("G = {{{0}}}", Structure.Select(ws => ws.Key).Ascending().Glue(", "));
        // Console.WriteLine("G = {{{0}}}", keys.Ascending().Glue(", "));
        // Console.WriteLine("All Rows");
        // foreach (var r in rows) Console.WriteLine(r.Ascending().Glue(","));
        // Console.WriteLine("All Columns");
        // foreach (var c in cols) Console.WriteLine(c.Ascending().Glue(","));
        // Console.WriteLine();

        Console.WriteLine("G = {{ {0} }}", Elements.Glue(", "));
        Console.WriteLine($"Order      : {keys.Count}");
        Console.WriteLine($"Is Group   : {isGroup}");
        Console.WriteLine($"Is Abelian : {isComm}");
        Console.WriteLine();

        var digits = Elements.Max(w => w.ToString().Length);
        var fmt = $"{{0,{digits}}}";
        Console.WriteLine("Table");
        foreach (var r in rows)
            Console.WriteLine(r.Glue(" ", fmt));

        Console.WriteLine();

        Console.WriteLine("Classes");
        foreach (var ws in Structure.OrderBy(a => a.Key))
            DisplayClasses(ws, fmt);

        Console.WriteLine();
        Console.WriteLine($"Total Words : {Structure.TotalWords}");
    }
    void DisplayClasses(WordSet ws, string fmt)
    {
        var reprs = string.Format(fmt.Replace("0,", "0,-"), Substitute(ws.Key));
        var sets = ws.Union(ws.Select(Substitute)).Ascending().ToHashSet();
        Console.WriteLine($"    {reprs} => {{ {sets.Glue(", ")} }}");
    }
    public static WordStructure Generate(params string[] relations)
    {
        Word.ResetCounter();
        var sw = Stopwatch.StartNew();
        var wg = new WordGroup(relations);
        sw.Stop();

        wg.DisplayElements();
        Console.WriteLine($"Total Time  : {sw.ElapsedMilliseconds} ms; Total Created Words : {Word.count}");
        Console.WriteLine();

        return wg.Structure;
    }
}
