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
        Structure = Structure.RewriteStruct(Structure);
    }

    WordStructure Structure { get; }

    public static WordStructure Generate(params string[] relations)
    {
        var sw = Stopwatch.StartNew();
        Word.count = 0;

        var wg = new WordGroup(relations);

        wg.Structure.DisplayReprs();
        wg.Structure.IsGroup();
        wg.Structure.GroupTable();
        Console.WriteLine();

        wg.Structure.Display();
        Console.WriteLine($"Total Time  : {sw.ElapsedMilliseconds} ms; Total Created Words : {Word.count}");
        return wg.Structure;
    }
}
