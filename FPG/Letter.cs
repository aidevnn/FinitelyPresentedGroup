namespace FPG;

public struct Letter : IEquatable<Letter>, IComparable<Letter>
{
    public char c { get; }
    public int pow { get; }
    public int sgn { get; }
    public int weight => pow * sgn;
    public char l => sgn == 1 ? c : char.ToUpper(c);
    public Letter(char c0, int p)
    {
        if (!char.IsLetter(c0) || !char.IsLower(c0) || p == 0)
            throw new Exception();

        c = c0;
        pow = p;
        sgn = p > 0 ? 1 : -1;
    }
    public Letter Invert() => new Letter(c, -pow);
    public IEnumerable<char> Extend() => Enumerable.Repeat(l, weight);

    public override int GetHashCode() => HashCode.Combine(c, pow);
    public override string ToString() => pow == 1 ? $"{c}" : $"{c}{pow}";
    (int, int, char) ToTuple() => (weight, -sgn, c);
    public bool Equals(Letter other) => c == other.c && pow == other.pow;
    public int CompareTo(Letter other) => this.ToTuple().CompareTo(other.ToTuple());
    public static implicit operator Letter((char c, int p) e) => new Letter(e.c, e.p);
    public static implicit operator Letter(char c) => char.IsLower(c) ? (c, 1) : (char.ToLower(c), -1);
}
