using FPG;

// var wstr = Relation.Structure("a2")
//             .Rewrite(Relation.Structure("b2"))
//             .Rewrite(Relation.Structure("c2"))
//             .Rewrite(Relation.Structure("bcbcbc"))
//             .Rewrite(Relation.Structure("acacac"))
//             .Rewrite(Relation.Structure("abab"));

// var wstr = Relation.Structure("a3")
//             .Rewrite(Relation.Structure("b4"))
//             .Rewrite(Relation.Structure("abab"));

// var wstr = Relation.Structure("a4").Rewrite(Relation.Structure("b2")).Rewrite(Relation.Structure("abab"));
// var wstr = Relation.Structure("a3").Rewrite(Relation.Structure("b2")).Rewrite(Relation.Structure("aba-1b-1"));

var wstr = Relation.Structure("a4")
            .Rewrite(Relation.Structure("a2b-2"))
            .Rewrite(Relation.Structure("a-1bab"));

var wstr0 = new WordStructure(wstr);
while (true)
{
    int sz0 = wstr0.Count;
    wstr0 = wstr0.Product();
    if (sz0 == wstr0.Count)
        break;
}

wstr0.DisplayKeys();
wstr0.GroupTable();
Console.WriteLine();

wstr0.Display();