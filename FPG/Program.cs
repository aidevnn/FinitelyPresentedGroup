using FPG;
using System.Diagnostics;
using System.Text.RegularExpressions;

WordStructure Generate(params string[] relations)
{
    var sw = Stopwatch.StartNew();
    WordStructureExt.count = 0;
    var wstr = WordStructureExt.MergeRelation(relations).LoopDevelop();
    wstr.DisplayReprs();
    wstr.IsGroup();
    wstr.GroupTable();
    Console.WriteLine();

    wstr.Display();
    Console.WriteLine($"Total Time  : {sw.ElapsedMilliseconds} ms; Total Created Words : {WordStructureExt.count}");

    return wstr;
}

// Generate("a4", "b3", "abab");

// Generate("a4", "b2", "abab"); // D4

// Generate("a2", "b3", "ababab", "abab2=bab2a"); // A4

// Generate("a6"); // C6

// Generate("a2", "b2", "ababab"); // S3

// Generate("a2", "b2", "c2", "aba = c", "ababab"); // S3

Generate("a2", "b2", "abab"); // Klein

Generate("a2", "b2", "abab"); // Klein

Generate("a3", "b2", "aba-1b-1"); // C6

Generate("a2", "b2", "ababab"); // S3

Generate("a4", "a2b-2", "b-1aba"); // H8

Generate("a3", "b6", "ab = ba"); // C3 x C6 

Generate("a2", "b2", "c2", "bcbcbc", "acacac", "abab"); // S4
