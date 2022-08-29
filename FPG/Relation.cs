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
    }

    public void Build()
    {
        HashSet<Word> eq = new() { Word.Empty };
        for (int k = 0; k < word.extStr.Length; ++k)
        {
            var s0 = word.extStr.Take(k).JoinChars();
            var s1 = word.extStr.Skip(k).JoinChars();
            var w0 = new Word(s1 + s0);
            eq.Add(w0);
        }

        structure = new WordStructure(new(eq), structure);
        foreach (var w in eq)
        {
            for (int k = 0; k < w.extStr.Length; ++k)
            {
                var s0 = w.extStr.Take(k).JoinChars();
                var s1 = w.extStr.Skip(k).JoinChars();
                var w0 = new Word(s0);
                var w1 = new Word(s1).Invert();
                var ws = new WordSet(new[] { w0, w1 });
                structure = new WordStructure(ws, structure);
            }
        }
    }

    public void Display()
    {
        Console.WriteLine($"Relation : {word}");
        structure.Display();
    }

    public static WordStructure Structure(string relation) => new Relation(relation).structure;
}
