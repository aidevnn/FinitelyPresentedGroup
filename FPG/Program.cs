using FPG;
using System.Diagnostics;

void Generate(params string[] relations)
{
    var sw = Stopwatch.StartNew();
    WordStructureExt.count = 0;
    if (relations.Length == 0) return;
    var nbGens = relations.SelectMany(r => r).Where(c => char.IsLetter(c)).Select(c => char.ToLower(c)).Distinct().Count();
    if (nbGens > relations.Count()) return;

    Queue<string> rels = new(relations);
    var wstr = Relation.Structure(rels.Dequeue());
    while (rels.Count != 0)
        wstr = wstr.RewriteStruct(Relation.Structure(rels.Dequeue()));

    while (true)
    {
        int sz0 = wstr.Count;
        wstr = wstr.Develop();
        if (sz0 == wstr.Count)
            break;
    }

    sw.Stop();

    wstr.DisplayReprs();
    wstr.IsGroup();
    wstr.GroupTable();
    Console.WriteLine();

    wstr.Display();
    Console.WriteLine($"Total Time  : {sw.ElapsedMilliseconds} ms; Total Created Words : {WordStructureExt.count}");
}

// Generate("a4", "b3", "abab");

// Generate("a4", "b2", "abab"); // D4

// Generate("a2", "b3", "ababab", "abab2=bab2a"); // A4

// Generate("a6"); // C6

// Generate("a2", "b2", "ababab"); // S3

// Generate("a2", "b2", "c2", "aba = c", "ababab"); // S3

// Generate("a2", "b2", "ababab"); // S3

// Generate("a2", "b2", "abab"); // Klein

// Generate("a3", "b2", "aba-1b-1"); // C6

// Generate("a4", "a2b-2", "b-1aba"); // H8

// Generate("a3", "b6", "ab = ba"); // C3 x C6 

// Generate("a2", "b2", "c2", "bcbcbc", "acacac", "abab"); // S4