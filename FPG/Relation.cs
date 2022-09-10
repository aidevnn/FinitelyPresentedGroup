namespace FPG;

public class Relation
{
    public Word word { get; }
    public WordStructure structure { get; private set; }
    public Relation(string relation)
    {
        var split = relation.Split('=');
        if (split.Length == 1)
            word = new(relation);
        else if (split.Length == 2)
        {
            var w0 = new Word(split[0]);
            var w1 = new Word(split[1]);
            word = new(w0.ToString() + w1.Invert().ToString());
        }
        else
            throw new Exception();

        structure = new();
        Build();
        // BuildMinimalist();
    }
    // Equivalents relations and maximum words equivalents that can be deduced. 
    public void Build()
    {
        HashSet<Word> eq = new() { Word.Empty, word };
        structure = new(new(eq), structure);
        for (int k = 0; k < word.extStr.Length; ++k)
        {
            var s0 = word.extStr.Take(k).Glue();
            var s1 = word.extStr.Skip(k).Glue();
            var w0 = new Word(s1 + s0);
            eq.Add(w0);
        }

        foreach (var w in eq)
        {
            for (int k = 1; k < w.extStr.Length; ++k)
            {
                var s0 = w.extStr.Take(k).Glue();
                var s1 = w.extStr.Skip(k).Glue();
                var w0 = new Word(s0);
                var w1 = new Word(s1);
                var ws0 = new WordSet(new[] { w0, w1.Invert() });
                structure = new(ws0, structure);
            }
        }
    }
    // Only generators and one relation, it works but slower
    public void BuildMinimalist()
    {
        HashSet<Word> eq = new() { Word.Empty, word };
        structure = new(new(eq), structure);

        var gens = word.SelectMany(l => l.Extend()).Where(char.IsLower).Select(c => new Word($"{c}")).Distinct();
        gens = gens.Concat(gens.Select(w => w.Invert())).Distinct();
        foreach (var w in gens)
            structure = new(new WordSet(w), structure);
    }

    public void Display()
    {
        Console.WriteLine($"Relation : {word}");
        structure.Display();
    }

    public static WordStructure Structure(string relation) => new Relation(relation).structure;
}
