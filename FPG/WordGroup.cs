using System.Diagnostics;

namespace FPG;

public class WordGroup
{
    public WordGroup(params string[] relations)
    {
        Structure = new();
        var gens = relations.SelectMany(r => r.Where(char.IsLetter)).Distinct().Ascending().ToArray();

        foreach (var r in relations)
            Structure = Structure.RewriteStruct(Relation.Structure(r));

        Name = string.Format("<({0})| {1} >", gens.Glue(","), relations.Glue(", "));

        Structure = Structure.LoopDevelop(5);
        Elements = Structure.Select(ws => ws.Key).Ascending().ToArray();
    }
    string Name { get; }
    WordStructure Structure { get; }
    Word[] Elements { get; }
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
                table[i, j] = e2;
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

        Console.WriteLine("G = {0}", Name);
        // if (isGroup) // uncomment for testing
        //     return;

        Console.WriteLine($"Order      : {keys.Count}");
        Console.WriteLine($"Is Group   : {isGroup}");
        Console.WriteLine($"Is Abelian : {isComm}");
        Console.WriteLine("G = {{ {0} }}", Elements.Select(w => w.extStr2).Glue(", "));
        Console.WriteLine();

        if (Elements.Length > 40)
        {
            Console.WriteLine("*** TOO HUGE ***");
            return;
        }

        var digits = Elements.Max(w => w.extStr2.Length);
        var fmt = $"{{0,{digits}}}";
        Console.WriteLine("Table");
        foreach (var r in rows)
            Console.WriteLine(r.Select(w => w.extStr2).Glue(" ", fmt));

        Console.WriteLine();

        Console.WriteLine("Classes");
        Structure.Display();
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
