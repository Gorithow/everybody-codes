
var line = Console.ReadLine();
var lines = new List<string>();

while (line != null && line != "")
{
    lines.Add(line);
    line = Console.ReadLine();
}

var s = (0, lines[0].IndexOf('.'));

var queue = new Queue<State>();
var visited = new HashSet<State>();
var dists = new Dictionary<State, int>();

var initialState = new State(s.Item1, s.Item2, new HashSet<char>());
queue.Enqueue(initialState);
dists[initialState] = 0;

var letters = lines.Where(x => x.Any(c => char.IsLetter(c))).SelectMany(x => x.Where(c => char.IsLetter(c))).ToHashSet();

while (queue.Any())
{
    var current = queue.Dequeue();

    if (visited.Contains(current))
    {
        continue;
    }

    visited.Add(current);

    var (x, y, set) = (current.X, current.Y, current.Set);

    var neighbours = new List<(int, int)>()
            {
                (x + 1, y),
                (x - 1, y),
                (x, y + 1),
                (x, y - 1)
            };

    foreach (var n in neighbours)
    {
        if (n.Item1 < 0 || n.Item1 >= lines.Count || n.Item2 < 0 || n.Item2 >= lines[0].Length)
        {
            continue;
        }

        if (lines[n.Item1][n.Item2] == '#' || lines[n.Item1][n.Item2] == '~')
        {
            continue;
        }

        var newSet = new HashSet<char>(set);
        if (letters.Contains(lines[n.Item1][n.Item2]) && !set.Contains(lines[n.Item1][n.Item2]))
        {
            newSet.Add(lines[n.Item1][n.Item2]);
        }

        var newState = new State(n.Item1, n.Item2, newSet);

        if (!dists.ContainsKey(newState))
        {
            dists[newState] = dists[current] + 1;
        }

        if (newSet.Count == letters.Count && n.Item1 == s.Item1 && n.Item2 == s.Item2)
        {
            Console.WriteLine(dists[newState]);
            return;
        }

        queue.Enqueue(newState);
    }
}

public class State
{
    public int X { get; }
    public int Y { get; }
    public HashSet<char> Set { get; }

    public State(int x, int y, HashSet<char> set)
    {
        X = x;
        Y = y;
        Set = new HashSet<char>(set);
    }

    public override bool Equals(object? obj)
    {
        if (obj is State other)
        {
            return X == other.X && Y == other.Y && Set.SetEquals(other.Set);
        }
        return false;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 31 + X.GetHashCode();
        hash = hash * 31 + Y.GetHashCode();
        foreach (var c in Set)
        {
            hash = hash * 31 + c.GetHashCode();
        }
        return hash;
    }

    public void Deconstruct(out int x, out int y, out HashSet<char> set)
    {
        x = X;
        y = Y;
        set = Set;
    }
}
