using FPG;
using System.Diagnostics;

void Generate(params string[] relations)
{
    var sw = Stopwatch.StartNew();
    if (relations.Length == 0) return;
    Queue<string> rels = new(relations);
    var wstr = Relation.Structure(rels.Dequeue());
    while (rels.Count != 0)
        wstr = wstr.Rewrite(Relation.Structure(rels.Dequeue()));

    WordStructureExt.loop = 0;
    while (true)
    {
        int sz0 = wstr.Count;
        wstr = wstr.Develop();
        if (sz0 == wstr.Count)
            break;
    }

    sw.Stop();

    wstr.DisplayReprs();
    wstr.GroupTable();
    Console.WriteLine();

    wstr.Display();
    Console.WriteLine($"Total Time : {sw.ElapsedMilliseconds} ms");
}

// Generate("a2", "b2", "c2", "bcbcbc", "acacac", "abab"); // S4

// Generate("a4", "b3", "abab");

// Generate("a4", "a2b-2", "b-1aba"); // H8

// Generate("a4", "b2", "abab"); // D4

// Generate("a3", "b2", "abab");

// Generate("a3", "b2", "aba-1b-1"); // C6

// Generate("a2", "b2", "abab"); // D2

// Generate("a3", "b6", "ab = ba"); // C3 x C6 

// Generate("a6"); // C6

Generate("a3", "b2", "aba-1b-1"); // C6

Generate("a4", "a2b-2", "b-1aba"); // H8

Generate("a3", "b6", "ab = ba"); // C3 x C6 